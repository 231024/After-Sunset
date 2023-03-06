public sealed class InputController : IExecute
    {
        //private readonly ISaveDataRepository _saveDataRepository;
        private readonly IUserInputProxy _horizontal;
        private readonly IUserInputProxy _vertical;
        private readonly IUserInputProxy _rotation;
        
        public InputController((IUserInputProxy inputHorizontal, IUserInputProxy inputVertical, IUserInputProxy _rotation) input)
        {
            _horizontal = input.inputHorizontal;
            _vertical = input.inputVertical;
            _rotation = input._rotation;
        }

        public void Execute(float deltaTime)
        {
            _horizontal.GetAxis();
            _vertical.GetAxis();
            _rotation.GetAxis();
        }
    }