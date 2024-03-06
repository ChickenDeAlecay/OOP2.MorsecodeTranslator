namespace User;

public class User
{
    public User(string name)
    {
        this.Name = name;
        this.Created = true;
    }
    public string Name { get; set; }

    public bool Created { get; set; }
}