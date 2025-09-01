using System.Collections.Generic;

namespace FlashCards.Models;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Dictionary<string, object> Preferences{ get; set; }
    public List<string> Favourites { get; set; }
}