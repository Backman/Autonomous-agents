
import java.util.*;
import java.awt.Point;

/**
 *
 * @author Johan Hagelbäck
 */
public class World 
{
    public float[][] totalPF;
    private float[][] agentTotalPF;
    public float[][] terrainPF;
   
    public int w = 50;
    public int h = 40;
    public int cellW = 15;
    
    public boolean useTrail = false;
    public boolean squadAttraction = false;
    
    public float scale = 0.7f;
    
    public Vector<WObject> enemies;
    public Vector<WObject> evade;
    public Vector<WObject> own;
    public Vector<WObject> goals;
    public Vector<Agent> agents;
    
    public World()
    {
        reset(1);
    }
    
    public void reset(int mapID)
    {
        totalPF = new float[w][h];
        terrainPF = new float[w][h];
        enemies = new Vector<WObject>();
        evade = new Vector<WObject>();
        own = new Vector<WObject>();
        goals = new Vector<WObject>();
        agents = new Vector<Agent>();
        
        MapGenerator.generateMap(this, mapID);
    }
    
    public void removeObject(int x, int y)
    {
        for (Agent a:agents)
        {
            if (a.x == x && a.y == y)
            {
                agents.remove(a);
                return;
            }
        }
        
        for (WObject o:enemies)
        {
            if (o.x == x && o.y == y)
            {
                enemies.remove(o);
                return;
            }
        }
        
        for (WObject o:evade)
        {
            if (o.x == x && o.y == y)
            {
                evade.remove(o);
                return;
            }
        }
        
        for (WObject o:own)
        {
            if (o.x == x && o.y == y)
            {
                own.remove(o);
                return;
            }
        }
        
        for (WObject o:goals)
        {
            if (o.x == x && o.y == y)
            {
                goals.remove(o);
                return;
            }
        }
    }
    
    public WObject findObject(int x, int y)
    {
        for (Agent a:agents)
        {
            if (a.x == x && a.y == y)
            {
                return a;
            }
        }
        
        for (WObject o:enemies)
        {
            if (o.x == x && o.y == y)
            {
                return o;
            }
        }
        
        for (WObject o:evade)
        {
            if (o.x == x && o.y == y)
            {
                return o;
            }
        }
        
        for (WObject o:own)
        {
            if (o.x == x && o.y == y)
            {
                return o;
            }
        }
        
        for (WObject o:goals)
        {
            if (o.x == x && o.y == y)
            {
                return o;
            }
        }
        
        return null;
    }
    
    public void makeMove()
    {
        //TODO: No tail or obstacle for the own agent
        for(Agent a:agents)
        {
            update(a);
            float bestPF = agentTotalPF[a.x][a.y];
            int bestX = a.x;
            int bestY = a.y;

            for (int x = a.x - 1; x <= a.x + 1; x++)
            {
                for (int y = a.y - 1; y <= a.y + 1; y++)
                {
                    if (x >= 0 && x < w && y >= 0 && y < h)
                    {
                        float cPF = agentTotalPF[x][y];
                        if (cPF >= bestPF && canMove(x, y, a))
                        {
                            bestPF = cPF;
                            bestX = x;
                            bestY = y;
                        }
                    }
                }
            }

            a.x = bestX;
            a.y = bestY;
            a.updateTail(new Point(bestX, bestY));

            update();
        }
    }
    
    private boolean canMove(int x, int y, Agent agent)
    {
        for (Agent a:agents)
        {
            if (agent.id != a.id)
            {
                if (a.x == x && a.y == y)
                {
                    return false;
                }
            }
        }
        
        for (WObject o:enemies)
        {
            if (o.x == x && o.y == y)
            {
                return false;
            }
        }
        
        for (WObject o:evade)
        {
            if (o.x == x && o.y == y)
            {
                return false;
            }
        }
        
        for (WObject o:own)
        {
            if (o.x == x && o.y == y)
            {
                return false;
            }
        }
        
        return true;
    }
    
    public void update()
    {
        totalPF = new float[w][h];
        update(totalPF, null);
    }
    
    public void update(Agent agent)
    {
        agentTotalPF = new float[w][h];
        update(agentTotalPF, agent);
    }
    
    private void update(float[][] pf, Agent agent)
    {
        //Enemy units
        for (WObject o:enemies)
        {
            int cSize = o.getSize();
            float[][] cPF = o.getPF();
            for (int x = 0; x < cSize; x++)
            {
                for (int y = 0; y < cSize; y++)
                {
                    int wX = o.x - cSize / 2 + x;
                    int wY = o.y - cSize / 2 + y;
                    if (wX >= 0 && wX < w && wY >= 0 && wY < h)
                    {
                        pf[wX][wY] += cPF[x][y] * scale;
                    }
                }
            }
        }
        
        //Evade enemies
        for (WObject o:evade)
        {
            int cSize = o.getSize();
            float[][] cPF = o.getPF();
            for (int x = 0; x < cSize; x++)
            {
                for (int y = 0; y < cSize; y++)
                {
                    int wX = o.x - cSize / 2 + x;
                    int wY = o.y - cSize / 2 + y;
                    if (wX >= 0 && wX < w && wY >= 0 && wY < h)
                    {
                        pf[wX][wY] += cPF[x][y] * scale;
                    }
                }
            }
        }
        
        //Goals
        for (WObject o:goals)
        {
            int cSize = o.getSize();
            float[][] cPF = o.getPF();
            for (int x = 0; x < cSize; x++)
            {
                for (int y = 0; y < cSize; y++)
                {
                    int wX = o.x - cSize / 2 + x;
                    int wY = o.y - cSize / 2 + y;
                    if (wX >= 0 && wX < w && wY >= 0 && wY < h)
                    {
                        pf[wX][wY] += cPF[x][y] * scale;
                    }
                }
            }
        }
        
        if (squadAttraction)
        {
            int cX = 0;
            int cY = 0;
            int cnt = 0;
            for (Agent a:agents)
            {
                cX += a.x;
                cY += a.y;
                cnt++;
            }
            cX = (int)Math.round(cX / cnt);
            cY = (int)Math.round(cY / cnt);
            
            int cSize = PF.getInstance().aSquad.length;
            float[][] cPF = PF.getInstance().aSquad;
            for (int x = 0; x < cSize; x++)
            {
                for (int y = 0; y < cSize; y++)
                {
                    int wX = cX - cSize / 2 + x;
                    int wY = cY - cSize / 2 + y;
                    if (wX >= 0 && wX < w && wY >= 0 && wY < h)
                    {
                        pf[wX][wY] += cPF[x][y] * scale;
                    }
                }
            }
        }
        
        //Own units
        for (WObject o:own)
        {
            int cSize = o.getSize();
            float[][] cPF = o.getPF();
            for (int x = 0; x < cSize; x++)
            {
                for (int y = 0; y < cSize; y++)
                {
                    int wX = o.x - cSize / 2 + x;
                    int wY = o.y - cSize / 2 + y;
                    if (wX >= 0 && wX < w && wY >= 0 && wY < h)
                    {
                        pf[wX][wY] += cPF[x][y];
                    }
                }
            }
        }
        
        //Agents
        for (Agent a:agents)
        {
            boolean add = false;
            if (agent == null) add = true;
            else if (agent.id != a.id) add = true;
            
            if (add)
            {
                int cSize = a.getSize();
                float[][] cPF = a.getPF();
                for (int x = 0; x < cSize; x++)
                {
                    for (int y = 0; y < cSize; y++)
                    {
                        int wX = a.x - cSize / 2 + x;
                        int wY = a.y - cSize / 2 + y;
                        if (wX >= 0 && wX < w && wY >= 0 && wY < h)
                        {
                            pf[wX][wY] += cPF[x][y];
                        }
                    }
                }
            }
            
            //Trail
            if (useTrail)
            {
                add = false;
                if (agent == null) add = true;
                else if (agent.id == a.id) add = true;
            
                if (add)
                {
                    for (int i = 0; i < a.trail.size(); i++)
                    {
                        pf[a.trail.elementAt(i).x][a.trail.elementAt(i).y] -= 20.0f;
                    }
                }
            }
        }
        
        //Terrain
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                pf[x][y] += terrainPF[x][y];
            }
        }
    }
    
    /**
     * Dumps the code for the current map.
     */
    public void dump()
    {
        int cSX = -1;
        
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                if (terrainPF[x][y] <= -15000 && cSX == -1)
                {
                    cSX = x;
                }
                if (terrainPF[x][y] > -15000 && cSX >= 0)
                {
                    int cEX = x - 1;
                    System.out.println("fill(world, " + cSX + ", " + cEX + ", " + y + ");");
                    cSX = -1;
                }
                if (x == w - 1 && cSX >= 0)
                {
                    int cEX = x;
                    System.out.println("fill(world, " + cSX + ", " + cEX + ", " + y + ");");
                    cSX = -1;
                }
            }
        }
        
        //Enemy units
        for (WObject o:enemies)
        {
            System.out.println("world.enemies.add(new Enemy(" + o.x + ", " + o.y + "));");
        }
        
        //Evade units
        for (WObject o:evade)
        {
            System.out.println("world.evade.add(new EvadeEnemy(" + o.x + ", " + o.y + "));");
        }
        
        //Goals
        for (WObject o:goals)
        {
            System.out.println("world.goals.add(new Goal(" + o.x + ", " + o.y + "));");
        }
        
        //Own units
        for (WObject o:own)
        {
            System.out.println("world.own.add(new Own(" + o.x + ", " + o.y + "));");
        }
        
        //Agents
        for (Agent a:agents)
        {
            System.out.println("world.agents.add(new Agent(" + a.x + ", " + a.y + "));");
        }
        
        System.out.println("world.scale = " + scale + "f;");
    }
}
