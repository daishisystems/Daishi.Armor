#region Includes

using System;
using System.Text;

#endregion

namespace Daishi.Armor {
    public class ArmorTokenGenerator : ICommand {
        private readonly ArmorToken armorToken;
        private readonly ArmorTokenGenerationStep first;

        public object Result { get { return ArmorTokenGenerationStepResult; } }
        public ArmorTokenGenerationStepResult ArmorTokenGenerationStepResult { get; private set; }

        public ArmorTokenGenerator(ArmorToken armorToken, ArmorTokenGenerationStep first) {
            this.armorToken = armorToken;
            this.first = first;
        }

        public void Execute() {
            first.Generate(armorToken);
            ArmorTokenGenerationStepResult = first.ArmorTokenGenerationStepResult;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }

    public abstract class ArmorTokenGenerationStep : ICommand {
        private readonly ArmorTokenGenerationStep next;

        public object Result { get; private set; }
        public ArmorTokenGenerationStepResult ArmorTokenGenerationStepResult { get; protected set; }

        protected ArmorTokenGenerationStep(ArmorTokenGenerationStep next) {
            this.next = next;
        }

        public abstract void Execute();

        public virtual void Generate(object armorToken) {
            Execute();
            next.Generate(ArmorTokenGenerationStepResult.Output);

            ArmorTokenGenerationStepResult = next.ArmorTokenGenerationStepResult;
        }

        public void Undo() {
            throw new NotImplementedException();
        }
    }

    public class ArmorTokenGenerationStepResult {
        public object Output { get; set; }
    }

    public class SerialiseArmorTokenGenerationStep : ArmorTokenGenerationStep {
        private readonly ArmorTokenSerialisor armorTokenSerialisor;

        public SerialiseArmorTokenGenerationStep(ArmorTokenSerialisor armorTokenSerialisor, ArmorTokenGenerationStep next) : base(next) {
            this.armorTokenSerialisor = armorTokenSerialisor;
        }

        public override void Execute() {
            armorTokenSerialisor.Execute();

            ArmorTokenGenerationStepResult = new ArmorTokenGenerationStepResult {
                Output = armorTokenSerialisor.SerialisedArmorToken
            };
        }
    }

    public class EmptyArmorTokenGenerationStep : ArmorTokenGenerationStep {
        private string armorToken;

        public EmptyArmorTokenGenerationStep() : base(null) {}

        public override void Execute() {
            ArmorTokenGenerationStepResult = new ArmorTokenGenerationStepResult {
                Output = armorToken
            };
        }

        public override void Generate(object armorToken) {
            this.armorToken = (string) armorToken;
            Execute();
        }
    }

    public class EncryptArmorTokenGenerationStep : ArmorTokenGenerationStep {
        private readonly EncryptionMechanismFactory encryptionMechanismFactory;
        private string armorToken;

        public EncryptArmorTokenGenerationStep(EncryptionMechanismFactory encryptionMechanismFactory, ArmorTokenGenerationStep next) : base(next) {
            this.encryptionMechanismFactory = encryptionMechanismFactory;
        }

        public override void Execute() {
            var encrytionMechanism = encryptionMechanismFactory.CreateEncryptionMechanism(Encoding.UTF8.GetBytes(armorToken));
            encrytionMechanism.Execute();

            ArmorTokenGenerationStepResult = new ArmorTokenGenerationStepResult {
                Output = encrytionMechanism.Output
            };
        }

        public override void Generate(object armorToken) {
            this.armorToken = (string) armorToken;
            base.Generate(armorToken);
        }
    }

    public class HashArmorTokenGenerationStep : ArmorTokenGenerationStep {
        private readonly HashingMechanismFactory hashingMechanismFactory;
        private byte[] armorToken;

        public HashArmorTokenGenerationStep(HashingMechanismFactory hashingMechanismFactory, ArmorTokenGenerationStep next) : base(next) {
            this.hashingMechanismFactory = hashingMechanismFactory;
        }

        public override void Execute() {
            var hashingMechanism = hashingMechanismFactory.CreateHashingMechanism(armorToken);
            hashingMechanism.Execute();

            ArmorTokenGenerationStepResult = new ArmorTokenGenerationStepResult {
                Output = hashingMechanism.Output
            };
        }

        public override void Generate(object armorToken) {
            this.armorToken = Convert.FromBase64String((string) armorToken);
            base.Generate(armorToken);
        }
    }
}