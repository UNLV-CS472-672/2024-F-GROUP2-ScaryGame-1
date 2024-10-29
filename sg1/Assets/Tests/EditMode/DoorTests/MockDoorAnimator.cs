using UnityEngine;
using Moq;

public class MockDoorAnimator
{
    // These two fields simulate internal state of AnimatorController
    public bool isClosed;
    public string currentStateName;
    private Mock<IAnimator> mock;
    public IAnimator Object => mock.Object;
    public MockDoorAnimator(bool isClosed, string currentStateName)
    {
        this.isClosed = isClosed;
        this.currentStateName = currentStateName;
        mock = new Mock<IAnimator>();

        // These define how the mock object (m) should respond to various function calls
        mock.Setup(m => m.CompareAnimatorStateName(It.IsAny<string>()))
            .Returns((string s) => s == this.currentStateName);
        mock.Setup(m => m.SetBool("isClosed", It.IsAny<bool>()))
            .Callback((string s, bool b) => this.isClosed = b);
        mock.Setup(m => m.GetBool("isClosed"))
            .Returns(() => this.isClosed); // This is a nullary lambda so that it get's evaluated when called
    }

}
