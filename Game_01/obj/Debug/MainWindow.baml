   M S B A M L   `   `   `  ����B  >Game_01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:Game_01Game_01  5   	       Game_01.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Game_01     5   	   U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5   	     � Title$  Game_01��5	   	   $	��450��6   $	��800��6&   .��5
      �� -   5        $��myCanvas��5
      |�. 
����ح� J� RenderTransformOrigin$ 0.589,0.586&�6s   .+�5   
   �� -   5        $��
background��5      $	��400��6&   $
��1262��63   
��� ���6@   �� 5   
   -   5        $��background2��5      $	��400��6'   $
��1262��64   
������ �� Left$
 1262��6S   �� 5   
   -   5        $��ground��5      $��32��6"   $	��800��6.   
���   �
 �� Top$	 387��6G   $ 0��6X   �� 5   
   -   5        $��player��5      $��99��6"   $��67��6.   
���**��$	 151��6F   $	 110��6W   �� 5   
   -   5        $��obstacle��5      $	��112��6$   $	��112��61   
���  ��$	 236��6H   $	 630��6Y   �� 5   
   -   5        $��	scoreText��5      $��22(�6!   $
��Bold"�6/   $��Score: 0��6A   �� 5   
   "  WpfAnimatedGif.ImageBehavior   RepeatBehavior$ Forever��6      AnimatedSource$ Resourses\car1.gif��6;   $	��108��6q   $	 487��6~   $	 240��6�   $	��138��6�   $��Resourses/car1.gif��6�   �� 5   
   $ Forever��6   $ Resourses\bonfire.gif��6;   $	��288��6t   $	 201��6�   $	��232��6�   $��Resourses/bonfire.gif��6�   $	 121��6�   �� 5   
   $ Forever��6   $ Resourses\obst2.gif��6;   $	��112��6r   $	 630��6   $	��112��6�   $��Resourses/obst2.gif��6�   $	 119��6�   5      5      