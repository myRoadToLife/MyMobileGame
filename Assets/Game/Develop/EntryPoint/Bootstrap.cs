using System.Collections;
using Game.Develop.DI;
using UnityEngine;

namespace Game.Develop.EntryPoint
{
    
    //Если Entry Point это глобальные регистрации для старта проекта
    //То Bootstrap уже для инициализации начала работ
    public class Bootstrap : MonoBehaviour
    {
        public IEnumerator Run(DiContainer container)
        {
            //Включаем загрузочную штору
            
            //Инициализация всех сервисов. Подгрузка данных и конфигов(реклама и аналитика)
            
            yield return new WaitForSeconds(1.5f);
            
            //скрываем штору
            
            //Переход на  следующую сцену с помощью сервиса смены сцен
        }
    }
}
