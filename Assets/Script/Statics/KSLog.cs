using UnityEngine;

namespace KemiaSimulatorCore.Script.Statics{
    public static class KSLog{
        private static void kslog_builder(this Object source, string trace, LogType log_type = LogType.Log){
            #if UNITY_EDITOR
            switch (log_type)
            {
                case LogType.Log:
                    Debug.Log(trace, source);
                    break;
                
                case LogType.Error:
                    Debug.LogError(trace, source);
                    break;
                
                case LogType.Warning:
                    Debug.LogWarning(trace, source);
                    break;
                
                case LogType.Assert:
                    Debug.LogAssertion(trace, source);
                    break;

                case LogType.Exception:
                    Debug.LogError("le log d'exception n'est pas supporté.", source);
                    break;
                
                default:
                    Debug.Log(trace, source);
                    break;
            }
            #endif
        }

        public static void kslog(this Object source, string trace){
            kslog_builder(source, trace);
        }
        
        public static void kslogwarn(this Object source, string trace){
            kslog_builder(source, trace, LogType.Warning);
        }
        
        public static void kslogerror(this Object source, string trace){
            kslog_builder(source, trace, LogType.Error);
        }

        public static void kslogassert(this Object source, string trace){
            kslog_builder(source, trace, LogType.Assert);
        }
    }
}