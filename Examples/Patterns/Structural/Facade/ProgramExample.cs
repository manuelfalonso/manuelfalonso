using UnityEngine;

namespace SombraStudios.Shared.Examples.Patterns.Structural.Facade
{
    // Credits to refactoring.guru for the original C# example
    public class ProgramExample : MonoBehaviour
    {
        private void Start()
        {
            // The client code may have some of the subsystem's objects already
            // created. In this case, it might be worthwhile to initialize the
            // Facade with these objects instead of letting the Facade create
            // new instances.
            Subsystem1 subsystem1 = new Subsystem1();
            Subsystem2 subsystem2 = new Subsystem2();
            Facade facade = new Facade(subsystem1, subsystem2);
            Client.ClientCode(facade);
        }

        // The Facade class provides a simple interface to the complex logic of one
        // or several subsystems. The Facade delegates the client requests to the
        // appropriate objects within the subsystem. The Facade is also responsible
        // for managing their lifecycle. All of this shields the client from the
        // undesired complexity of the subsystem.
        public class Facade
        {
            private Subsystem1 _subsystem1;
            private Subsystem2 _subsystem2;

            public Facade(Subsystem1 subsystem1, Subsystem2 subsystem2)
            {
                _subsystem1 = subsystem1;
                _subsystem2 = subsystem2;
            }

            // The Facade's methods are convenient shortcuts to the sophisticated
            // functionality of the subsystems. However, clients get only to a
            // fraction of a subsystem's capabilities.
            public void Operation()
            {
                Debug.Log("Facade initializes subsystems:");
                _subsystem1.Operation1();
                _subsystem2.Operation1();
                Debug.Log("Facade orders subsystems to perform the action:");
                _subsystem1.OperationN();
                _subsystem2.OperationZ();
            }
        }

        // The Subsystem can accept requests either from the facade or client
        // directly. In any case, to the Subsystem, the Facade is yet another
        // client, and it's not a part of the Subsystem.
        public class Subsystem1
        {
            public void Operation1()
            {
                Debug.Log("Subsystem1: Ready!");
            }
            public void OperationN()
            {
                Debug.Log("Subsystem1: Go!");
            }
        }

        // Some facades can work with multiple subsystems at the same time.
        public class Subsystem2
        {
            public void Operation1()
            {
                Debug.Log("Subsystem2: Get ready!");
            }
            public void OperationZ()
            {
                Debug.Log("Subsystem2: Fire!");
            }
        }

        public class Client
        {
            // The client code works with complex subsystems through a simple
            // interface provided by the Facade. When a facade manages the lifecycle
            // of the subsystem, the client might not even know about the existence
            // of the subsystem. This approach lets you keep the complexity under
            // control.
            public static void ClientCode(Facade facade)
            {
                // ...
                facade.Operation();
                // ...
            }
        }
    }
}
