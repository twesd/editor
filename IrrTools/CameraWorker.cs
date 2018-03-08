using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serializable;
using IrrlichtWrap;

namespace IrrTools
{
    public class CameraWorker
    {
        /// <summary>
        /// Класс для работы с irrlicht
        /// </summary>
        IrrDevice _irrDevice;

        public CameraWorker(IrrDevice irrDevice)
        {
            _irrDevice = irrDevice;
        }

        /// <summary>
        /// Создать описание камеры
        /// </summary>
        /// <returns></returns>
        public ContainerCamera CreateContainerCamera()
        {
            ContainerCamera container = new ContainerCamera()
            {
                Position = Convertor.CreateVertex(_irrDevice.DeviceW.Camera.Position),
                Target = Convertor.CreateVertex(_irrDevice.DeviceW.Camera.Target)
            };
            return container;
        }

        /// <summary>
        /// Обновить положение камеры
        /// </summary>
        public void ApplyContainer(ContainerCamera containerCamera)
        {
            if (containerCamera == null) return;

            _irrDevice.DeviceW.Camera.Position = Convertor.CreateVertex(containerCamera.Position);
            _irrDevice.DeviceW.Camera.Target = Convertor.CreateVertex(containerCamera.Target);
        }
    }
}
