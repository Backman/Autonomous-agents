
import java.awt.*;
import javax.swing.*;
import java.awt.event.*;
import java.util.Vector;
import javax.swing.event.*;

/**
 *
 * @author Johan Hagelbäck
 */
public class GUI extends JPanel implements ActionListener, MouseMotionListener, MouseListener, ChangeListener
{
    private PFpanel pfpanel;
    private JPopupMenu pp;
    private JButton paintBT;
    private JButton eraseBT;
    private JButton attBT;
    private JButton gridBT;
    private JButton trailBT;
    private JSlider scale;
    private JComboBox<MaplistItem> mapList;
    private MaplistItem[] mapItems;
     
    private World world;
    
    private WObject toMove = null;
    private boolean isPainting = false;
    private boolean isErasing = false;
    
    public GUI()
    {
        world = new World();
        world.update();
        
        initComponents();
    }
    
    private void initComponents()
    {
        mapItems = new MaplistItem[9];
        mapItems[0] = new MaplistItem(1, "Pathfinding");
        mapItems[1] = new MaplistItem(2, "Surround enemy");
        mapItems[2] = new MaplistItem(3, "Retreat");
        mapItems[3] = new MaplistItem(4, "Multiple enemies");
        mapItems[4] = new MaplistItem(5, "Local optima");
        mapItems[5] = new MaplistItem(6, "Narrow passage");
        mapItems[6] = new MaplistItem(7, "Squad formation");
        mapItems[7] = new MaplistItem(8, "Difficult regions");
        mapItems[8] = new MaplistItem(0, "Empty");
        
        setLayout(new FlowLayout());
        setPreferredSize(new Dimension(960, 640));
        
        pfpanel = new PFpanel(world);
        add(pfpanel);
        
        pfpanel.addMouseListener(this);
        pfpanel.addMouseMotionListener(this);
        
        
        JPanel rPanel = new JPanel();
        rPanel.setPreferredSize(new Dimension(170, 600));
        
        JPanel jp = new JPanel();
        jp.setPreferredSize(new Dimension(165,65));
        jp.setBorder(BorderFactory.createTitledBorder("Actions"));
        JButton bt = new JButton("Move Agents");
        bt.setActionCommand("Move");
        bt.setForeground(Color.blue);
        bt.addActionListener(this);
        jp.add(bt);
        rPanel.add(jp);
        
        jp = new JPanel();
        jp.setPreferredSize(new Dimension(165,95));
        jp.setBorder(BorderFactory.createTitledBorder("Map"));
        mapList = new JComboBox(mapItems);
        mapList.setPreferredSize(new Dimension(150,25));
        jp.add(mapList);
        
        bt = new JButton("Reset Map");
        bt.setActionCommand("Reset");
        bt.addActionListener(this);
        jp.add(bt);
        rPanel.add(jp);
        
        jp = new JPanel();
        jp.setPreferredSize(new Dimension(165,100));
        jp.setBorder(BorderFactory.createTitledBorder("Edit"));
        paintBT = new JButton("Edit Terrain");
        paintBT.setActionCommand("paint");
        paintBT.addActionListener(this);
        jp.add(paintBT);
        
        eraseBT = new JButton("Erase Terrain");
        eraseBT.setActionCommand("erase");
        eraseBT.addActionListener(this);
        jp.add(eraseBT);
        rPanel.add(jp);
        
        jp = new JPanel();
        jp.setPreferredSize(new Dimension(165,65));
        jp.setBorder(BorderFactory.createTitledBorder("Color Scale"));
        scale = new JSlider(JSlider.HORIZONTAL, 5, 30, 10);
        scale.setPreferredSize(new Dimension(160, 35));
        scale.addChangeListener(this);
        scale.setValue((int)(world.scale * 10));
        jp.add(scale);
        rPanel.add(jp);
        
        jp = new JPanel();
        jp.setPreferredSize(new Dimension(165,195));
        jp.setBorder(BorderFactory.createTitledBorder("Settings"));
        JLabel l = new JLabel("Pheromone trail:");
        jp.add(l);
        trailBT = new JButton("Off");
        trailBT.setActionCommand("trail");
        trailBT.addActionListener(this);
        jp.add(trailBT);
        
        l = new JLabel("Squad grouping field:");
        jp.add(l);
        attBT = new JButton("Off");
        attBT.setActionCommand("attracting");
        attBT.addActionListener(this);
        attBT.setEnabled(false);
        jp.add(attBT);
        
        l = new JLabel("Grid lines:", SwingConstants.CENTER);
        l.setPreferredSize(new Dimension(120, 16));
        jp.add(l);
        gridBT = new JButton("Off");
        gridBT.setActionCommand("grid");
        gridBT.addActionListener(this);
        jp.add(gridBT);
        
        rPanel.add(jp);
        
        bt = new JButton("Dump");
        bt.setActionCommand("dump");
        bt.addActionListener(this);
        //rPanel.add(bt);
        
        add(rPanel);
        
        pp = new JPopupMenu();
    }
    
    public void stateChanged(ChangeEvent e)
    {
        float cScale = (float)(scale.getValue() / 10.0f);
        
        if (cScale != world.scale)
        {
            world.scale = cScale;
            world.update();
            pfpanel.updateUI();
            pfpanel.repaint();
        }
    }
    
    public void actionPerformed(ActionEvent e)
    {
        String cmd = e.getActionCommand();
        if (cmd.equalsIgnoreCase("Move"))
        {
            world.makeMove();
            pfpanel.updateUI();
            pfpanel.repaint();
        }
        if (cmd.equalsIgnoreCase("Reset"))
        {
            MaplistItem mi = (MaplistItem)mapList.getSelectedItem();
            world.reset(mi.id);
            if (world.useTrail) trailBT.setText("On");
            else trailBT.setText("Off");
            if (world.squadAttraction) attBT.setText("On");
            else attBT.setText("Off");
            world.update();
            scale.setValue((int)(world.scale * 10));
            if (world.agents.size() > 1)
            {
                attBT.setEnabled(true);
            }
            else
            {
                attBT.setEnabled(false);
            }
            pfpanel.updateUI();
            pfpanel.repaint();
        }
        if (cmd.equalsIgnoreCase("paint"))
        {
            if (!isErasing)
            {
                isPainting = !isPainting;
                if (isPainting)
                {
                    eraseBT.setEnabled(false);
                    paintBT.setText("Stop editing");
                }
                else
                {
                    eraseBT.setEnabled(true);
                    paintBT.setText("Edit Terrain");
                }
            }
        }
        if (cmd.equalsIgnoreCase("erase"))
        {
            if (!isPainting)
            {
                isErasing = !isErasing;
                if (isErasing)
                {
                    paintBT.setEnabled(false);
                    eraseBT.setText("Stop erasing");
                }
                else
                {
                    paintBT.setEnabled(true);
                    eraseBT.setText("Erase Terrain");
                }
            }
        }
        if (cmd.equalsIgnoreCase("dump"))
        {
            world.dump();
        }
        if (cmd.equalsIgnoreCase("trail"))
        {
            world.useTrail = !world.useTrail;
            if (world.useTrail) trailBT.setText("On");
            else trailBT.setText("Off");
            world.update();
            pfpanel.updateUI();
            pfpanel.repaint();
        }
        if (cmd.equalsIgnoreCase("attracting"))
        {
            world.squadAttraction = !world.squadAttraction;
            if (world.squadAttraction) attBT.setText("On");
            else attBT.setText("Off");
            world.update();
            pfpanel.updateUI();
            pfpanel.repaint();
        }
        if (cmd.equalsIgnoreCase("grid"))
        {
            pfpanel.showGrid = !pfpanel.showGrid;
            if (pfpanel.showGrid) gridBT.setText("On");
            else gridBT.setText("Off");
            world.update();
            pfpanel.updateUI();
            pfpanel.repaint();
        }
        if (cmd.startsWith("AddEnemy"))
        {
            String[] tokens = cmd.split(" ");
            try
            {
                int cX = Integer.parseInt(tokens[1]);
                int cY = Integer.parseInt(tokens[2]);
                world.enemies.add(new Enemy(cX, cY));
                world.update();
                pfpanel.updateUI();
                pfpanel.repaint();
            }
            catch (Exception ex)
            {
                ex.printStackTrace();
            }
        }
        if (cmd.startsWith("AddEvade"))
        {
            String[] tokens = cmd.split(" ");
            try
            {
                int cX = Integer.parseInt(tokens[1]);
                int cY = Integer.parseInt(tokens[2]);
                world.evade.add(new EvadeEnemy(cX, cY));
                world.update();
                pfpanel.updateUI();
                pfpanel.repaint();
            }
            catch (Exception ex)
            {
                ex.printStackTrace();
            }
        }
        if (cmd.startsWith("AddOwn"))
        {
            String[] tokens = cmd.split(" ");
            try
            {
                int cX = Integer.parseInt(tokens[1]);
                int cY = Integer.parseInt(tokens[2]);
                world.own.add(new Own(cX, cY));
                world.update();
                pfpanel.updateUI();
                pfpanel.repaint();
            }
            catch (Exception ex)
            {
                ex.printStackTrace();
            }
        }
        if (cmd.startsWith("AddGoal"))
        {
            String[] tokens = cmd.split(" ");
            try
            {
                int cX = Integer.parseInt(tokens[1]);
                int cY = Integer.parseInt(tokens[2]);
                world.goals.add(new Goal(cX, cY));
                world.update();
                pfpanel.updateUI();
                pfpanel.repaint();
            }
            catch (Exception ex)
            {
                ex.printStackTrace();
            }
        }
        if (cmd.startsWith("AddAgent"))
        {
            String[] tokens = cmd.split(" ");
            try
            {
                int cX = Integer.parseInt(tokens[1]);
                int cY = Integer.parseInt(tokens[2]);
                world.agents.add(new Agent(cX, cY));
                world.update();
                pfpanel.updateUI();
                pfpanel.repaint();
            }
            catch (Exception ex)
            {
                ex.printStackTrace();
            }
        }
        if (cmd.startsWith("Remove"))
        {
            String[] tokens = cmd.split(" ");
            try
            {
                int cX = Integer.parseInt(tokens[1]);
                int cY = Integer.parseInt(tokens[2]);
                world.removeObject(cX, cY);
                world.update();
                pfpanel.updateUI();
                pfpanel.repaint();
            }
            catch (Exception ex)
            {
                ex.printStackTrace();
            }
        }
    }
    
    public void mouseMoved(MouseEvent e)
    {
        
    }
    
    public void mouseDragged(MouseEvent e)
    {
        int eX = e.getX();
        int eY = e.getY();
        int nX = (int)((double)eX / world.cellW);
        int nY = (int)((double)eY / world.cellW);
        
        if (isPainting)
        {
            if (nX >= 0 && nX < world.w && nY >= 0 && nY < world.h)
            {
                world.terrainPF[nX][nY] = -15000;
                MapGenerator.postFill(world, nX, nY);
                world.update();
                  
                pfpanel.updateUI();
                pfpanel.repaint();
            }
        }
        else if (isErasing)
        {
            if (nX >= 0 && nX < world.w && nY >= 0 && nY < world.h)
            {
                if (world.terrainPF[nX][nY] <= -15000)
                {
                    world.terrainPF[nX][nY] = 0;
                    MapGenerator.postFill(world);
                    world.update();

                    pfpanel.updateUI();
                    pfpanel.repaint();
                }
            }
        }
        else
        {
            if (toMove == null)
            {
                toMove = world.findObject(nX, nY);
            }

            if (toMove != null)
            {
                toMove.x = nX;
                toMove.y = nY;
                
                if (toMove instanceof Agent)
                {
                    Agent a = (Agent)toMove;
                    a.trail.clear();
                    a.trail.add(new Point(nX, nY));
                }

                world.update();

                pfpanel.updateUI();
                pfpanel.repaint();
            }
        } 
    }
    
    public void mouseClicked(MouseEvent e)
    {
        if (SwingUtilities.isLeftMouseButton(e))
        {
            int eX = e.getX();
            int eY = e.getY();
            int nX = (int)((double)eX / world.cellW);
            int nY = (int)((double)eY / world.cellW);

            if (isPainting)
            {
                world.terrainPF[nX][nY] = -15000;
                MapGenerator.postFill(world, nX, nY);
                world.update();

                pfpanel.updateUI();
                pfpanel.repaint();
            }
            else if (isErasing)
            {
                if (world.terrainPF[nX][nY] <= -15000)
                {
                    world.terrainPF[nX][nY] = 0;
                    MapGenerator.postFill(world);
                    world.update();

                    pfpanel.updateUI();
                    pfpanel.repaint();
                }
            }
        }
    }
    
    public void mouseEntered(MouseEvent e)
    {
        
    }
    
    public void mouseExited(MouseEvent e)
    {
        
    }
    
    public void mouseReleased(MouseEvent e)
    {
        toMove = null;
    }
    
    public void mousePressed(MouseEvent e)
    {
        if (SwingUtilities.isRightMouseButton(e))
        {
            int eX = e.getX();
            int eY = e.getY();
            int nX = (int)((double)eX / world.cellW);
            int nY = (int)((double)eY / world.cellW);
        
            if (world.terrainPF[nX][nY] > -15000)
            {
                pp.removeAll();
                JMenuItem mi = new JMenuItem("  (" + nX + "," + nY + ")");
                mi.setEnabled(false);
                pp.add(mi);
                pp.addSeparator();
                mi = new JMenuItem("Add enemy");
                mi.setActionCommand("AddEnemy " + nX + " " + nY);
                mi.addActionListener(this);
                pp.add(mi);
                mi = new JMenuItem("Add evade enemy");
                mi.setActionCommand("AddEvade " + nX + " " + nY);
                mi.addActionListener(this);
                pp.add(mi);
                mi = new JMenuItem("Add own unit");
                mi.setActionCommand("AddOwn " + nX + " " + nY);
                mi.addActionListener(this);
                pp.add(mi);
                mi = new JMenuItem("Add agent");
                mi.setActionCommand("AddAgent " + nX + " " + nY);
                mi.addActionListener(this);
                pp.add(mi);
                mi = new JMenuItem("Add goal");
                mi.setActionCommand("AddGoal " + nX + " " + nY);
                mi.addActionListener(this);
                pp.add(mi);
                
                WObject w = world.findObject(nX, nY);
                if (w != null)
                {
                    pp.addSeparator();
                    mi = new JMenuItem("Remove object");
                    mi.setActionCommand("Remove " + nX + " " + nY);
                    mi.addActionListener(this);
                    pp.add(mi);
                }

                pp.show(e.getComponent(), e.getX(), e.getY());
            }
        }
    }
}