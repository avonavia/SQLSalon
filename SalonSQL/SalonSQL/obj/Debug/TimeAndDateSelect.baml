   M S B A M L   `   `   `  ����C  ?SalonSQL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SalonSQLSalonSQL  5   	        SalonSQL.TimeAndDateSelect   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:SalonSQL     5   	     � 
ResizeMode$  NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � Title$+ %Выбор даты и времени��5
      $	��450��6"   $	��800��6/   .��5      � .+�5   
   �� ���	#FF904DB0$
��Left=�6%   $��54��6@   
���   �$	��Top=�6[   $	��793��6s   $��2��6   �� 5   
   ���	#FF904DB0$
��Left=�6%   $��54��6@   
���   �$	��Top=�6[   $	��793��6s   $��0,367,0,-0.333q�6   $��2��6�   � 5   
   $��0,0,0.333,53.667q�6   .+�5      �� -   5         ��Name$ Select_Date_Button��5      $��Выбрать��61   $
��Left=�6C   $��650,325,0,10q�6^   $	��100��6t   6�   &   System.Windows.Controls.Calendar  5      -   5          Name$ Calendar��5      $
��Left=�6)   $	��173��6D   $��267,126,0,0q�6Q   $	��Top=�6f   $	��266��6~      DisplayMode$ Month=�6�      SelectionMode$ 
SingleDate=�6�      BlackoutDates 5      5      �� 5      -   5         ��Name$ showDateBox��5      $
��Left=�6-   $��31��6H   $��419,304,0,0q�6T   $
��Wrap=�6i   $	��Top=�6}   $��68��6�   �� 5      .+�5      �� -   5        	 ��Name$	 TimeList��5      $��600,130,100,0q�6-   5      �� 5      $"��Выбранная дата:��6   $
��Left=�6.   $��36��6I   $��311,299,0,0q�6U   $	��Top=�6j   $	��103��6�   �� 5      $"��Выберите время:��6   $
��Left=�6.   $��26��6I   $��
596,94,0,0q�6U   $	��Top=�6i   $	��113��6�   5      �� 5   
   $:��4Выберите дату и время приёма��6   $
��Left=�67   $��54��6R   $��	223,0,0,0q�6^   $	��Top=�6q   $	��383��6�   $��25(�6�   �� 5   
   $��54��6   $��718,0,29.667,0q�6   $	��Top=�64   $��logo.png��6L   5      5!      