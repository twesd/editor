using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// Имена фукций доступных в контроле
    /// </summary>
    public class ParserFunctionNames
    {
        /// <summary>
        /// Получить имена функций используемых в выражениях контролов
        /// </summary>
        /// <returns></returns>
        public static List<string> GetControlsNames()
        {
            List<string> names = new List<string>();
            
            names.Add("GetControlVisible");            
            names.Add("GetControlParameter");
            names.Add("GetControlPosition");
            names.Add("GetControlAngle");
            names.Add("GetControlState");
            
            names.Add("SetControlVisible");
            names.Add("SetControlParameter");
            names.Add("SetControlPosition");
            names.Add("SetControlPackage");
            names.Add("SetControlClip");
            names.Add("SetControlText");

            names.Add("RemoveControl");

            names.Add("ControlClickedUp");            

            return names;
        }

        /// <summary>
        /// Получить имена функций используемых в выражениях юнита
        /// </summary>
        /// <returns></returns>
        public static List<string> GetUnitsNames()
        {
            List<string> names = new List<string>();
            names.Add("DebugBreak");
            names.Add("ExtractInt");

            // функции встроенные в string расширение
            names.Add("parseInt");
            names.Add("parseFloat");            

            names.Add("GetData");
            names.Add("GetNowTime");        
            names.Add("GetDirectRotation");
            names.Add("GetNodeRotation");
            names.Add("GetNodeAbsoluteRotation");
            names.Add("GetNodePosition");
            names.Add("GetNodeVisible");
            names.Add("GetNodeId");            
            names.Add("GetSceneNodeExist");
            names.Add("GetGlobalParameter");
            names.Add("GetUserSetting");
            names.Add("GetThisNode");
            names.Add("GetThisUnitInstance");            
            names.Add("GetUnitInstanceParameter");
            names.Add("GetUnitInstanceByNode");
            names.Add("GetUnitInstanceCreator");
            names.Add("GetThisUnitInstance");                       

            names.Add("SetData");
            names.Add("SetUnitInstanceParameter");
            names.Add("SetUnitInstanceCandidateNextAction");
            names.Add("SetGlobalParameter");
            names.Add("SetUserSetting");
            names.Add("SetNodeRotation");
            names.Add("SetNodePosition");
            names.Add("SetNodeVisible");

            names.Add("StartUnitManager");
            names.Add("StopUnitManager");
            names.Add("StartTimer");
            names.Add("StopTimer");
            names.Add("ShowAd");
            names.Add("HideAd");

            names.Add("CompleteStage");
            names.Add("RestartStage");
            names.Add("LoadStage");
            names.Add("SaveUserSettings");

            names.Add("RateApp");

            names.Add("DrawImage");

            return names;
        }

        /// <summary>
        /// Получить имена для параметров
        /// </summary>
        /// <returns></returns>
        public static List<string> GetParametersNames()
        {
            List<string> names = new List<string>();
            names.Add("ThisParam");
            names.Add("NewValue");
            names.Add("Result");
            return names;
        }
    }
}
