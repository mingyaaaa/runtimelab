// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// CGENSYS.H -
//
// Generic header for choosing system-dependent helpers
//



#ifndef __cgensys_h__
#define __cgensys_h__

class MethodDesc;
class Stub;
class Thread;
class CrawlFrame;
struct EE_ILEXCEPTION_CLAUSE;
struct TransitionBlock;
struct VASigCookie;
struct CORCOMPILE_EXTERNAL_METHOD_THUNK;
class ComPlusCallMethodDesc;

#include <cgencpu.h>


#ifdef EnC_SUPPORTED
void ResumeAtJit(PT_CONTEXT pContext, LPVOID oldFP);
#endif

#if defined(TARGET_X86)
void ResumeAtJitEH   (CrawlFrame* pCf, BYTE* startPC, EE_ILEXCEPTION_CLAUSE *EHClausePtr, DWORD nestingLevel, Thread *pThread, BOOL unwindStack);
int  CallJitEHFilter (CrawlFrame* pCf, BYTE* startPC, EE_ILEXCEPTION_CLAUSE *EHClausePtr, DWORD nestingLevel, OBJECTREF thrownObj);
void CallJitEHFinally(CrawlFrame* pCf, BYTE* startPC, EE_ILEXCEPTION_CLAUSE *EHClausePtr, DWORD nestingLevel);
#endif // TARGET_X86

#ifdef FEATURE_COMINTEROP
extern "C" UINT32 STDCALL CLRToCOMWorker(TransitionBlock * pTransitionBlock, ComPlusCallMethodDesc * pMD);
extern "C" void GenericComPlusCallStub(void);

extern "C" void GenericComCallStub(void);
#endif // FEATURE_COMINTEROP

// The GC mode for the thread that initially called ThePreStub().
enum class CallerGCMode
{
    Unknown,
    Coop,
    Preemptive    // (e.g. UnmanagedCallersOnlyAttribute)
};

// Non-CPU-specific helper functions called by the CPU-dependent code
extern "C" PCODE STDCALL PreStubWorker(TransitionBlock * pTransitionBlock, MethodDesc * pMD);

extern "C" void STDCALL VarargPInvokeStubWorker(TransitionBlock * pTransitionBlock, VASigCookie * pVASigCookie, MethodDesc * pMD);
extern "C" void STDCALL VarargPInvokeStub(void);
extern "C" void STDCALL VarargPInvokeStub_RetBuffArg(void);

extern "C" void STDCALL GenericPInvokeCalliStubWorker(TransitionBlock * pTransitionBlock, VASigCookie * pVASigCookie, PCODE pUnmanagedTarget);
extern "C" void STDCALL GenericPInvokeCalliHelper(void);

extern "C" PCODE STDCALL ExternalMethodFixupWorker(TransitionBlock * pTransitionBlock, TADDR pIndirection, DWORD sectionIndex, Module * pModule);
extern "C" void STDCALL ExternalMethodFixupStub(void);
extern "C" void STDCALL ExternalMethodFixupPatchLabel(void);

extern "C" void STDCALL VirtualMethodFixupStub(void);
extern "C" void STDCALL VirtualMethodFixupPatchLabel(void);

extern "C" void STDCALL TransparentProxyStub(void);
extern "C" void STDCALL TransparentProxyStub_CrossContext();
extern "C" void STDCALL TransparentProxyStubPatchLabel(void);

#ifdef FEATURE_READYTORUN
extern "C" void STDCALL DelayLoad_MethodCall();

extern "C" void STDCALL DelayLoad_Helper();
extern "C" void STDCALL DelayLoad_Helper_Obj();
extern "C" void STDCALL DelayLoad_Helper_ObjObj();
#endif

// Returns information about the CPU processor.
// Note that this information may be the least-common-denominator in the
// case of a multi-proc machine.

#ifdef TARGET_X86
void GetSpecificCpuInfo(CORINFO_CPU * cpuInfo);
#else
inline void GetSpecificCpuInfo(CORINFO_CPU * cpuInfo)
{
    LIMITED_METHOD_CONTRACT;
    cpuInfo->dwCPUType = 0;
    cpuInfo->dwFeatures = 0;
    cpuInfo->dwExtendedFeatures = 0;
}

#endif // !TARGET_X86

#if (defined(TARGET_X86) || defined(TARGET_AMD64)) && !defined(CROSSGEN_COMPILE)
#ifdef TARGET_UNIX
// MSVC directly defines intrinsics for __cpuid and __cpuidex matching the below signatures
// We define matching signatures for use on Unix platforms.

extern "C" void __stdcall __cpuid(int cpuInfo[4], int function_id);
extern "C" void __stdcall __cpuidex(int cpuInfo[4], int function_id, int subFunction_id);
#endif // TARGET_UNIX
extern "C" DWORD __stdcall xmmYmmStateSupport();
#endif

inline bool TargetHasAVXSupport()
{
#if (defined(TARGET_X86) || defined(TARGET_AMD64)) && !defined(CROSSGEN_COMPILE)
    int cpuInfo[4];
    __cpuid(cpuInfo, 0x00000001);           // All x86/AMD64 targets support cpuid.
    return ((cpuInfo[3] & (1 << 28)) != 0); // The AVX feature is ECX bit 28.
#endif // (defined(TARGET_X86) || defined(TARGET_AMD64)) && !defined(CROSSGEN_COMPILE)
    return false;
}

#ifdef FEATURE_PREJIT
// Can code compiled for "minReqdCpuType" be used on "actualCpuType"
inline BOOL IsCompatibleCpuInfo(const CORINFO_CPU * actualCpuInfo,
                                const CORINFO_CPU * minReqdCpuInfo)
{
    LIMITED_METHOD_CONTRACT;
    return ((minReqdCpuInfo->dwFeatures & actualCpuInfo->dwFeatures) ==
             minReqdCpuInfo->dwFeatures);
}
#endif // FEATURE_PREJIT


#ifndef DACCESS_COMPILE
// Given an address in a slot, figure out if the prestub will be called
BOOL DoesSlotCallPrestub(PCODE pCode);
#endif

#ifdef DACCESS_COMPILE

// Used by dac/strike to make sense of non-jit/non-jit-helper call targets
// generated by the runtime.
BOOL GetAnyThunkTarget (T_CONTEXT *pctx, TADDR *pTarget, TADDR *pTargetMethodDesc);

#endif // DACCESS_COMPILE



//
// ResetProcessorStateHolder saves/restores processor state around calls to
// CoreLib during exception handling.
//
class ResetProcessorStateHolder
{
#if defined(TARGET_AMD64)
    ULONG m_mxcsr;
#endif

public:

    ResetProcessorStateHolder ()
    {
#if defined(TARGET_AMD64)
        m_mxcsr = _mm_getcsr();
        _mm_setcsr(0x1f80);
#endif // TARGET_AMD64
    }

    ~ResetProcessorStateHolder ()
    {
#if defined(TARGET_AMD64)
        _mm_setcsr(m_mxcsr);
#endif // TARGET_AMD64
    }
};


#endif // !__cgensys_h__
