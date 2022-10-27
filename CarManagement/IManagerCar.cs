using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement
{
    public interface IManagerCar
    {
        List<Car> AddCar(List<Car> Cars);
        List<Car> DeleteCar(List<Car> Cars);
        void ViewCar(List<Car> Cars);
        List<Car> UpdateCar(List<Car> Cars);
    }
}
