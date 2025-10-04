using System.Runtime.CompilerServices;

namespace QuickTests.CompilerServices;

[TestClass]
public class CallerMemberNameTests
{
    [TestMethod]
    public void FromPropertySetter()
    {
        // ---------- ARRANGE ----------
        var helper = new MyHelper
        {
            // ---------- ACT --------------
            //Using the set (or in this case, the init-) accessor
            MyProperty = "New Property Value"
        };

        // ---------- ASSERT -----------
        Assert.AreEqual(nameof(helper.MyProperty), helper.LastCallerMemberName);
    }

    [TestMethod]
    public void FromPropertyGetter()
    {
        // ---------- ARRANGE ----------
        var helper = new MyHelper();

        // ---------- ACT --------------
        _ = helper.MyProperty;

        // ---------- ASSERT -----------
        Assert.AreEqual(nameof(helper.MyProperty), helper.LastCallerMemberName);
    }

    [TestMethod]
    public void FromMethod()
    {
        // ---------- ARRANGE ----------
        var helper = new MyHelper();

        // ---------- ACT --------------
        helper.MyPublicMethod();

        // ---------- ASSERT -----------
        Assert.AreEqual(nameof(helper.MyPublicMethod), helper.LastCallerMemberName);
    }

    [TestMethod]
    public void FromExternal()
    {
        // ---------- ARRANGE ----------
        var helper = new MyHelper();

        // ---------- ACT --------------
        helper.SetLastCallerMemberName();

        // ---------- ASSERT -----------
        Assert.AreEqual(nameof(FromExternal), helper.LastCallerMemberName);
    }

    [TestMethod]
    public void FromExplicit()
    {
        // ---------- ARRANGE ----------
        const string name = "MyName";
        var helper = new MyHelper();

        // ---------- ACT --------------
        helper.SetLastCallerMemberName(name);

        // ---------- ASSERT -----------
        Assert.AreEqual(name, helper.LastCallerMemberName);
    }

    private class MyHelper
    {
        private string _myProperty = "Hello World!";

        /// <summary>
        /// Property where the getter and setter both call the <see cref="SetLastCallerMemberName"/> method.
        /// </summary>
        public string MyProperty
        {
            get
            {
                SetLastCallerMemberName();
                return _myProperty;
            }
            set
            {
                SetLastCallerMemberName();
                _myProperty = value;
            }
        }

        public void MyPublicMethod() => SetLastCallerMemberName();

        /// <summary>
        /// Holds the result of the latest <see cref="SetLastCallerMemberName"/> call.
        /// </summary>
        public string? LastCallerMemberName { get; private set; }

        /// <summary>
        /// Uses the <see cref="CallerMemberNameAttribute"/> to extract the caller member name, and writes the result to <see cref="LastCallerMemberName"/>.
        /// </summary>
        public void SetLastCallerMemberName([CallerMemberName] string? name = null)
        {
            LastCallerMemberName = name;
        }
    }
}