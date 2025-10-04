namespace QuickTests.DataTypes.Enums.Flags;

public class PermissionsHasFlagTestData : EnumHasFlagTestData<Permissions>
{
    protected override IEnumerable<(Permissions Value, Permissions FlagToCheck, bool ExpectedResult)> GetTestData()
    {
        // If permissions is NONE, we expect none of the flags to be set
        yield return (Permissions.None, Permissions.Read, false);
        yield return (Permissions.None, Permissions.Write, false);
        yield return (Permissions.None, Permissions.Execute, false);
        yield return (Permissions.None, Permissions.ReadWrite, false);
        yield return (Permissions.None, Permissions.All, false);

        // If permissions is ALL, we expect all the flags to be set
        yield return (Permissions.All, Permissions.Read, true);
        yield return (Permissions.All, Permissions.Write, true);
        yield return (Permissions.All, Permissions.Execute, true);
        yield return (Permissions.All, Permissions.ReadWrite, true);
        yield return (Permissions.All, Permissions.All, true);

        // If a single permission is set, only that permissions should be true
        yield return (Permissions.Read, Permissions.Read, true);
        yield return (Permissions.Read, Permissions.Write, false);
        yield return (Permissions.Read, Permissions.Execute, false);
        yield return (Permissions.Read, Permissions.ReadWrite, false);
        yield return (Permissions.Read, Permissions.All, false);

        yield return (Permissions.Write, Permissions.Read, false);
        yield return (Permissions.Write, Permissions.Write, true);
        yield return (Permissions.Write, Permissions.Execute, false);
        yield return (Permissions.Write, Permissions.ReadWrite, false);
        yield return (Permissions.Write, Permissions.All, false);

        yield return (Permissions.Execute, Permissions.Read, false);
        yield return (Permissions.Execute, Permissions.Write, false);
        yield return (Permissions.Execute, Permissions.Execute, true);
        yield return (Permissions.Execute, Permissions.ReadWrite, false);
        yield return (Permissions.Execute, Permissions.All, false);

        //If a combined permission is set, each individual set permission should return true
        yield return (Permissions.ReadWrite, Permissions.Read, true);
        yield return (Permissions.ReadWrite, Permissions.Write, true);
        yield return (Permissions.ReadWrite, Permissions.Execute, false);
        yield return (Permissions.ReadWrite, Permissions.ReadWrite, true);
        yield return (Permissions.ReadWrite, Permissions.All, false);
    }
}