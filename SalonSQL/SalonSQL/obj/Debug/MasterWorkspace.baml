   M S B A M L   `   `   `  ����C  ?SalonSQL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SalonSQLSalonSQL  5   	       SalonSQL.MasterWorkspace   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:SalonSQL     5   	     � 
ResizeMode$  NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � Title$ Окно мастера��5
      $	��450��6   $	��800��6'   .��5      � .+�5   
   �� ���	#FF904DB0$
��Left=�6%   $��54��6@   
���   �$	��Top=�6[   $	��793��6s   $��2��6   �� 5   
   ���	#FF904DB0$
��Left=�6%   $��54��6@   
���   �$	��Top=�6[   $	��793��6s   $��0,367,0,-0.333q�6   $��2��6�   � 5   
   $��0,0,0.333,53.667q�6   .+�5      �� .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ Grid_��5         SelectionMode$ Extended=�6*      SelectionUnit$ FullRow=�6C      AutoGenerateColumns .  $	��256��6x      
IsReadOnly . $��13(�6�   $��10,85,122.333,0q�6�      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID Клиента��6D   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6-   �� *ID+6D     5      $	 Имя Клиента��6F   
 6-   �� *Name+6F     5      $#	 Фамилия Клиента��6I   
 6-   �� *	Surname+6I     5      $%	 Отчество Клиента��6K   
 6-   �� *	Last_name+6K     5      $	 Пол Клиента��6H   
 6-   �� *Gender+6H     5      $	 Дата приёма��6G   
 6-   �� *Date_+6G     5      $	 Время приёма��6G   
 6-   �� *Time_+6G   5      �� 5      -   5         ��Name$ Exit_button��5      $��
Выйти��6.   $
��Left=�6>   $��31��6Y   $��680,-30,0,0q�6e   $	��100��6z   6�   �� 5      -   5        $ Get_service_button��5      $#��Показать услуги��65   $
��Left=�6O   $��58��6j   $��680,-455, 0, 0q�6v   $	��100��6�   6�   �� 5      -   5        $ Delete_button��5      $��Выполнить��60   $
��Left=�6D   $��58��6_   $��680,-310,0,0q�6k   $	��100��6�   6�   �� 5      -   5         ��Name$ SwitchOrdersList��5      $��Выполненные��65   $
��Left=�6K   $��44��6f   $��680,-150,0,0q�6r   $	��112��6�   6�   5      5       �� 5!   
   $��54��6   $��718,0,29.667,0q�6   $	��Top=�64   $��logo.png��6L   5"      5#      