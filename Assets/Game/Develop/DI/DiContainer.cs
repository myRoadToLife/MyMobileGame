using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Develop.DI
{
    public class DiContainer
    {
        private readonly Dictionary<Type, Registration> _container = new Dictionary<Type, Registration>();
        private readonly DiContainer _parent;
        private readonly List<Type> _requests = new List<Type>();

        public DiContainer() : this(null)
        {
        }

        public DiContainer(DiContainer parent) => _parent = parent;

        public void RegisterAsSingle <T>(Func<DiContainer, T> factory)
        {
            if (_container.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"The type {typeof(T)} is already registered.");

            Registration registration = new Registration(container => factory(this));
            _container[typeof(T)] = registration;
        }

        public T Resolve <T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new InvalidOperationException($"The type {typeof(T)} is already registered.");

            _requests.Add(typeof(T));

            try
            {
                if (_container.TryGetValue(typeof(T), out Registration registration))
                    return CreateFrom<T>(registration);

                if (_parent != null)
                    return _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"The type {typeof(T)} is not registered.");
        }

        private T CreateFrom <T>(Registration registration)
        {
            if (registration.Instance == null && registration.Factory != null)
                registration.Instance = registration.Factory(this);

            return (T)registration.Instance;
        }

        public class Registration
        {
            public Func<DiContainer, object> Factory { get; set; } //Спосмоб создания
            public object Instance { get; set; } //Объект, который создаем

            public Registration(object instance) => Instance = instance;

            public Registration(Func<DiContainer, object> factory) => Factory = factory;
        }
    }
}
