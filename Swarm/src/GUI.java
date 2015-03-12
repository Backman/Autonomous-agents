/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */


import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

/**
 *
 * @author admin
 */
public class GUI extends JPanel implements ActionListener, Runnable, MouseListener
{
    private WorldPanel wPanel;
    private JPanel buttonsPanel; 
    private JSlider speed;
    private JTextField noAgentsField;
    private boolean running = false;
    
    public GUI()
    {
        initComponents();
    }
    
    private void initComponents()
    {
        setLayout(new FlowLayout());
        
        buttonsPanel = new JPanel();
        buttonsPanel.setPreferredSize(new Dimension(180, 390));
        buttonsPanel.setLayout(new FlowLayout());
        
        //Buttons
        JButton bt;
        bt = new JButton("Reset");
        bt.addActionListener(this);
        bt.setPreferredSize(new Dimension(160, 25));
        buttonsPanel.add(bt);
        JLabel jl = new JLabel("No agents:", SwingConstants.CENTER);
        jl.setPreferredSize(new Dimension(90, 25));
        buttonsPanel.add(jl);
        noAgentsField = new JTextField("50");
        noAgentsField.setPreferredSize(new Dimension(60, 25));
        buttonsPanel.add(noAgentsField);
        bt = new JButton("Run");
        bt.addActionListener(this);
        bt.setPreferredSize(new Dimension(160, 25));
        buttonsPanel.add(bt);
        bt = new JButton("Stop");
        bt.addActionListener(this);
        bt.setPreferredSize(new Dimension(160, 25));
        buttonsPanel.add(bt);
        
        JPanel jp = new JPanel();
        jp.setPreferredSize(new Dimension(160, 25));
        buttonsPanel.add(jp);
        
        speed = new JSlider(JSlider.HORIZONTAL, 1, 20, 10);
        speed.setToolTipText("Speed");
        speed.setPreferredSize(new Dimension(160, 35));
        buttonsPanel.add(speed);
        
        add(buttonsPanel);
        
        wPanel = new WorldPanel();
        wPanel.addMouseListener(this);
        add(wPanel);
    }
    
    public void mousePressed(MouseEvent e)
    {
        if (running)
        {
            int eX = e.getX();
            int eY = e.getY();
            
            AgentSwarm.getInstance().setGoal(eX, eY);
        }
    }
    
    public void mouseExited(MouseEvent e) {}
    public void mouseEntered(MouseEvent e) {}
    public void mouseReleased(MouseEvent e) {}
    public void mouseClicked(MouseEvent e) {}
    
    public void actionPerformed(ActionEvent e)
    {
        String cmd = e.getActionCommand();
        
        if (cmd.equalsIgnoreCase("Reset"))
        {
            if (!running)
            {
                int noAgents = 10;
                try
                {
                    noAgents = Integer.parseInt(noAgentsField.getText());
                }
                catch (Exception ex)
                {
                    noAgents = 10;
                }
                AgentSwarm.getInstance().NO_AGENTS = noAgents;
                AgentSwarm.getInstance().generateRandom();
                wPanel.updateUI();
                wPanel.repaint();
            }
        }
        if (cmd.equalsIgnoreCase("Run"))
        {
            if (!running)
            {
                Thread thr = new Thread(this);
                thr.start();
            }
        }
        if (cmd.equalsIgnoreCase("Stop"))
        {
            running = false;
        }
    }
    
    public void run()
    {
        running = true;
        while (running)
        {
            try
            {
                AgentSwarm.getInstance().update();
                wPanel.updateUI();
                wPanel.repaint();
                Thread.sleep(speed.getValue());
            }
            catch (Exception ex)
            {
                
            }
        }
        speed.setValue(10);
        running = false;
    }
}
