   M S B A M L   `   `   `  ����C  ?UsersApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:UsersAppUsersApp  5   	       UsersApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:UsersApp     5   	     � Title$  Авторизация��5   	   $	��600��6   $
��1000��6*    � WindowStyle$
 None=�5	   	    � AllowsTransparency . 
������  � 	Resources 5       �� 
TargetType �� Property �� Value��%[   '��       �� 5   
   " ��6   .�5      ��  � ��$ Fonts/#Jost)�6+   5      5      .��5      � .+�5   
   �� -   5        $��275,0,275,0q�5   .   
�������$��Center=�6V    �� Padding$ 30q�6q   $	��450��6~    �� CornerRadius$ 30��6�   $	��480��6�   	 J� RenderTransformOrigin$	 0.503,0.682&�6�   
 J� Effect
 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 80Q�6#      Color$ DarkGray��63      ShadowDepth$ 0Q�6D   ._�5      �� $��Center=�6   .+�5      �� -   5        $��Right=�5   .   $��20��6J   $��20��6U   
������ 
������ $��0q�6�   .��5      �� $:��4C:\Users\HONOR\Downloads\free-icon-close-4013407.png��6   6\   5      �� 5      $��VECTOR��6   $��30(�6*   $
��Bold"�68   $��	0 30 0 10q�6J   $��Center=�6]   �� 5      $&�� Добро пожаловать!��6   $��30(�65   $��Medium"�6C   $��0 0 0 10q�6W   $��Center=�6i   �� 5       $	��252��6   .+�5!      �� $��Fonts/#Jost)�6   $��18(�65   $��Light"�6C   
�������$��30��6i   $��	45 10 0 0q�6u   .��6�   Введите данные6�   �� 5"      -   5         ��Name$ 	textBox_1��5"      $��Fonts/#Jost)�62   $��16(�6K   $��Light"�6Y   
�������$	��300��6~   
�������$��0,0,0,1q�6�   $��	50 0 50 5q�6�   $��30��6�   �� 5$      $��Fonts/#Jost)�6   $��18(�66   $��Light"�6D   
�������$��30��6i   $��45 0 0 0q�6u   .��6�   Введите пароль6�   G� 5%      -   5         G�Name$ 
password_1��5%   "   $	��300��67   
�������$��0,0,0,1q�6[   $��	50 0 50 0q�6v   $��30��6�   $��Fonts/#Raleway)�6�   $��14(�6�   
�������6�   5'      � 5(      .+�5)      �� -   5        $��Center=�5)   8   $��
Войти��6U   
���   �$	��294��6x   $��35��6�   
�������$��Fonts/#Raleway)�6�   
������ $��0q�6�   $��
0,-70,0,70q�6�   5*      5+      5,      5-      5.      