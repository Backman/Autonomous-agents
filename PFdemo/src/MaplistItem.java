/**
 *
 * @author Johan Hagelbäck (johan.hagelback@gmail.com)
 */
public class MaplistItem 
{
    public int id;
    public String name;
    
    public MaplistItem(int id, String name)
    {
        this.id = id;
        this.name = name;
    }
    
    public String toString()
    {
        return name;
    }
}
