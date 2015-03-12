/**
 *
 * @author Johan Hagelbäck (johan.hagelback@gmail.com)
 */
public class MapGenerator 
{
    public static int agent_ID = 0;
    
    public static void generateMap(World world, int mapID)
    {
        world.useTrail = false;
        world.squadAttraction = false;
        
        switch(mapID)
        {
            case 0:
                generateMap0(world);
                break;
            case 1:
                generateMap1(world);
                break;
            case 2:
                generateMap2(world);
                break;
            case 3:
                generateMap3(world);
                break;
            case 4:
                generateMap4(world);
                break;
            case 5:
                generateMap5(world);
                break;
            case 6:
                generateMap6(world);
                break;
            case 7:
                generateMap7(world);
                break;
            case 8:
                generateMap8(world);
                break;
            default:
                generateMap1(world);
                break;
        }
        postFill(world);
    }
    
    public static void generateMap0(World world)
    {
        world.agents.add(new Agent(2, 2));
        
        world.goals.add(new Goal(40,23));
    }
    
    public static void generateMap1(World world)
    {
        fill(world, 13, 28, 0);
        fill(world, 13, 28, 1);
        fill(world, 14, 28, 2);
        fill(world, 14, 27, 3);
        fill(world, 15, 26, 4);
        fill(world, 18, 24, 5);
        fill(world, 19, 24, 6);
        fill(world, 20, 23, 7);
        fill(world, 6, 12, 11);
        fill(world, 5, 12, 12);
        fill(world, 4, 13, 13);
        fill(world, 4, 14, 14);
        fill(world, 3, 14, 15);
        fill(world, 3, 14, 16);
        fill(world, 31, 35, 16);
        fill(world, 3, 13, 17);
        fill(world, 26, 37, 17);
        fill(world, 3, 13, 18);
        fill(world, 24, 40, 18);
        fill(world, 3, 12, 19);
        fill(world, 22, 40, 19);
        fill(world, 4, 12, 20);
        fill(world, 21, 40, 20);
        fill(world, 4, 12, 21);
        fill(world, 21, 40, 21);
        fill(world, 5, 12, 22);
        fill(world, 21, 40, 22);
        fill(world, 7, 11, 23);
        fill(world, 21, 39, 23);
        fill(world, 8, 10, 24);
        fill(world, 21, 39, 24);
        fill(world, 21, 39, 25);
        fill(world, 21, 38, 26);
        fill(world, 21, 37, 27);
        fill(world, 22, 36, 28);
        fill(world, 23, 34, 29);
        fill(world, 29, 31, 30);
        world.goals.add(new Goal(36, 8));
        world.own.add(new Own(6, 4));
        world.own.add(new Own(10, 26));
        world.agents.add(new Agent(7, 29));
        
        world.scale = 2.2f;
    }
    
    public static void generateMap2(World world)
    {
        fill(world, 17, 30, 0);
        fill(world, 18, 30, 1);
        fill(world, 18, 29, 2);
        fill(world, 20, 26, 3);
        fill(world, 22, 23, 4);
        fill(world, 29, 32, 26);
        fill(world, 26, 33, 27);
        fill(world, 24, 34, 28);
        fill(world, 23, 36, 29);
        fill(world, 23, 37, 30);
        fill(world, 23, 37, 31);
        fill(world, 23, 37, 32);
        fill(world, 22, 37, 33);
        fill(world, 22, 37, 34);
        fill(world, 22, 37, 35);
        fill(world, 23, 36, 36);
        fill(world, 24, 35, 37);
        fill(world, 27, 32, 38);
        world.enemies.add(new Enemy(32, 15));
        world.agents.add(new Agent(11, 11));
        world.agents.add(new Agent(11, 14));
        world.agents.add(new Agent(11, 17));
        
        world.scale = 1.7f;
    }
    
    public static void generateMap3(World world)
    {
        fill(world, 17, 30, 0);
        fill(world, 18, 30, 1);
        fill(world, 18, 29, 2);
        fill(world, 20, 26, 3);
        fill(world, 22, 23, 4);
        fill(world, 29, 32, 26);
        fill(world, 26, 33, 27);
        fill(world, 24, 34, 28);
        fill(world, 23, 36, 29);
        fill(world, 23, 37, 30);
        fill(world, 23, 37, 31);
        fill(world, 23, 37, 32);
        fill(world, 22, 37, 33);
        fill(world, 22, 37, 34);
        fill(world, 22, 37, 35);
        fill(world, 23, 36, 36);
        fill(world, 24, 35, 37);
        fill(world, 27, 32, 38);
        world.evade.add(new EvadeEnemy(32, 14));
        world.agents.add(new Agent(25, 12));
        world.agents.add(new Agent(25, 14));
        world.agents.add(new Agent(25, 16));
        
        world.scale = 1.8f;
    }
    
    public static void generateMap4(World world)
    {
        fill(world, 17, 30, 0);
        fill(world, 18, 30, 1);
        fill(world, 18, 29, 2);
        fill(world, 20, 26, 3);
        fill(world, 22, 23, 4);
        fill(world, 25, 32, 31);
        fill(world, 23, 34, 32);
        fill(world, 22, 37, 33);
        fill(world, 22, 37, 34);
        fill(world, 22, 37, 35);
        fill(world, 23, 36, 36);
        fill(world, 24, 35, 37);
        fill(world, 27, 32, 38);
        world.enemies.add(new Enemy(37, 10));
        world.enemies.add(new Enemy(28, 20));
        world.enemies.add(new Enemy(41, 23));
        world.agents.add(new Agent(16, 15));
        world.agents.add(new Agent(18, 9));
        world.agents.add(new Agent(17, 12));
        
        world.scale = 0.7f;
    }
    
    public static void generateMap5(World world)
    {
        fill(world, 18, 20, 8);
        fill(world, 14, 21, 9);
        fill(world, 13, 21, 10);
        fill(world, 13, 22, 11);
        fill(world, 12, 22, 12);
        fill(world, 12, 22, 13);
        fill(world, 12, 22, 14);
        fill(world, 12, 21, 15);
        fill(world, 12, 21, 16);
        fill(world, 12, 22, 17);
        fill(world, 12, 22, 18);
        fill(world, 12, 22, 19);
        fill(world, 12, 22, 20);
        fill(world, 12, 22, 21);
        fill(world, 12, 23, 22);
        fill(world, 12, 23, 23);
        fill(world, 12, 23, 24);
        fill(world, 12, 23, 25);
        fill(world, 13, 23, 26);
        fill(world, 13, 22, 27);
        fill(world, 14, 18, 28);
        world.goals.add(new Goal(42, 18));
        world.agents.add(new Agent(5, 18));
        world.scale = 2.2f;
    }
    
    public static void generateMap6(World world)
    {
        fill(world, 15, 18, 7);
        fill(world, 11, 19, 8);
        fill(world, 10, 20, 9);
        fill(world, 10, 20, 10);
        fill(world, 10, 20, 11);
        fill(world, 11, 21, 12);
        fill(world, 11, 21, 13);
        fill(world, 11, 21, 14);
        fill(world, 12, 20, 15);
        fill(world, 12, 18, 16);
        fill(world, 12, 18, 19);
        fill(world, 11, 20, 20);
        fill(world, 10, 20, 21);
        fill(world, 10, 21, 22);
        fill(world, 10, 21, 23);
        fill(world, 9, 22, 24);
        fill(world, 8, 22, 25);
        fill(world, 8, 22, 26);
        fill(world, 8, 22, 27);
        fill(world, 9, 21, 28);
        fill(world, 11, 20, 29);
        fill(world, 15, 17, 30);
        world.goals.add(new Goal(41, 17));
        world.agents.add(new Agent(5, 17));
        world.scale = 2.2f;
    }
    
    public static void generateMap7(World world)
    {
        world.enemies.add(new Enemy(29, 18));
        world.agents.add(new Agent(12, 11));
        world.agents.add(new Agent(7, 17));
        world.agents.add(new Agent(9, 19));
        world.agents.add(new Agent(10, 15));
        world.agents.add(new Agent(5, 14));
        world.agents.add(new Agent(8, 10));
        world.scale = 1.7f;
        world.squadAttraction = true;
    }
    
    public static void generateMap8(World world)
    {
        fill(world, 0, 19, 0);
        fill(world, 37, 42, 0);
        fill(world, 0, 15, 1);
        fill(world, 0, 11, 2);
        fill(world, 0, 8, 3);
        fill(world, 0, 6, 4);
        fill(world, 0, 5, 5);
        fill(world, 0, 4, 6);
        fill(world, 0, 3, 7);
        fill(world, 43, 45, 7);
        fill(world, 0, 2, 8);
        fill(world, 40, 49, 8);
        fill(world, 0, 1, 9);
        fill(world, 38, 49, 9);
        fill(world, 0, 1, 10);
        fill(world, 37, 49, 10);
        fill(world, 0, 1, 11);
        fill(world, 36, 49, 11);
        fill(world, 0, 1, 12);
        fill(world, 15, 20, 12);
        fill(world, 35, 49, 12);
        fill(world, 0, 2, 13);
        fill(world, 12, 26, 13);
        fill(world, 33, 49, 13);
        fill(world, 0, 2, 14);
        fill(world, 11, 49, 14);
        fill(world, 0, 3, 15);
        fill(world, 10, 49, 15);
        fill(world, 0, 3, 16);
        fill(world, 10, 23, 16);
        fill(world, 28, 49, 16);
        fill(world, 0, 3, 17);
        fill(world, 10, 19, 17);
        fill(world, 29, 49, 17);
        fill(world, 0, 2, 18);
        fill(world, 10, 18, 18);
        fill(world, 30, 49, 18);
        fill(world, 0, 2, 19);
        fill(world, 11, 16, 19);
        fill(world, 31, 49, 19);
        fill(world, 0, 0, 20);
        fill(world, 11, 15, 20);
        fill(world, 35, 49, 20);
        fill(world, 12, 15, 21);
        fill(world, 37, 49, 21);
        fill(world, 13, 14, 22);
        fill(world, 38, 49, 22);
        fill(world, 38, 49, 23);
        fill(world, 38, 49, 24);
        fill(world, 38, 49, 25);
        fill(world, 5, 8, 26);
        fill(world, 37, 49, 26);
        fill(world, 3, 10, 27);
        fill(world, 37, 49, 27);
        fill(world, 2, 10, 28);
        fill(world, 35, 49, 28);
        fill(world, 0, 11, 29);
        fill(world, 33, 49, 29);
        fill(world, 0, 11, 30);
        fill(world, 33, 49, 30);
        fill(world, 0, 12, 31);
        fill(world, 32, 49, 31);
        fill(world, 0, 12, 32);
        fill(world, 32, 49, 32);
        fill(world, 0, 14, 33);
        fill(world, 30, 49, 33);
        fill(world, 0, 17, 34);
        fill(world, 28, 49, 34);
        fill(world, 0, 18, 35);
        fill(world, 28, 47, 35);
        fill(world, 0, 18, 36);
        fill(world, 27, 45, 36);
        fill(world, 0, 45, 37);
        fill(world, 0, 44, 38);
        fill(world, 0, 41, 39);
        world.goals.add(new Goal(22, 6));
        world.agents.add(new Agent(24, 21));
        world.scale = 2.2f;
    }
    
    public static void fill(World world, int sX, int eX, int y)
    {
        for (int x = sX; x <= eX; x++)
        {
            world.terrainPF[x][y] = -15000;
        }
    }
    
    public static void postFill(World world)
    {
        //First, erase any spread potentials
        for (int x = 0; x < world.w; x++)
        {
            for (int y = 0; y < world.h; y++)
            {
                if (world.terrainPF[x][y] > -15000 && world.terrainPF[x][y] < 0)
                {
                    world.terrainPF[x][y] = 0;
                }
            }
        }
        
        for (int x = 0; x < world.w; x++)
        {
            for (int y = 0; y < world.h; y++)
            {
                if (world.terrainPF[x][y] == -15000)
                {
                    postFill(world, x, y);
                }
            }
        }
    }
    
    public static void postFill(World world, int cX, int cY)
    {
        for (int x = cX - 1; x <= cX + 1; x++)
        {
            for (int y = cY - 1; y <= cY + 1; y++)
            {
                if (x >= 0 && x < world.w && y >= 0 && y < world.h)
                {
                    if (world.terrainPF[x][y] >= 0)
                    {
                        world.terrainPF[x][y] = -20.0f;
                    }
                }
            }
        }
    }
}
