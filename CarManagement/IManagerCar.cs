using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement
{
    public interface IManagerCar
    {
        void AddCar();
        void DeleteCar();
        void ViewCar();
        void UpdateCar();
    }
}
