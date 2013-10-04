#region Includes

using System;

#endregion

namespace Daishi.Armor {
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
}