   M S B A M L   `   `   `  ����C  ?UsersApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:UsersApp.UserControl.TaskUsersApp.UserControl.Task  5      .    (UsersApp.UserControl.Task.TaskDetailPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:UsersApp.UserControl.Task     5      .��5	      ��   �� CornerRadius$  25��6   ���	#FFFAFAFA
������ $��2q�6P   $��10,0,0,0q�6d   ._�5
   
   � 
������ $��20,31,49,27q�6(    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��Детали задачи��6%   $��24(�6:   $��#Jost)�6H   $
��Bold"�6[   $��	8,10,0,20q�6m   $
��Left=�6�   $��2��6�   �� 5      $��1��6   $#��Название задачи��6%   $��25��6<   $��#Jost)�6H   $��16(�6[   $��	10,0,10,0q�6i   $��2��6|   �� 5      $��2��6   $��25��6;   $��#Jost)�6G   $��16(�6Z   $��
10,0,10,10q�6h   $��2��6|   ��6%   �� *Name+6|   �� 5      $��3��6   $#��Описание задачи��6%   $��25��6<   $��#Jost)�6H   $��16(�6[   $��	10,0,10,0q�6i   $��2��6|   �� 5      $��4��6   $��25��6B   $��#Jost)�6N   $��16(�6a   $��
10,0,10,10q�6o   $��2��6�   ��6%   �� *Description+6�   �� 5!      $��5��6   $��0��6%   $��Исполнитель��65   $��#Jost)�6H   $��16(�6[   $��10,0,0,0q�6i   �� 5"      $��5��6   $��1��6%   $��Статус��65   $��#Jost)�6C   $��16(�6V   $��	10,0,10,0q�6d   �� 5#      $��6��6   $��0��6%   $��25��6S   $��#Jost)�6_   $��16(�6r   $��	10,0,5,10q�6�   ��65   �� *Employee.FIO+6�   �� 5$      $��6��6   $��1��6%   $��25��6R   $��#Jost)�5%      $��16(�6   $��	5,0,10,10q�6-   ��5$   5   �� *Status.Name+5%   -   �� 5(      $��6��6   $��0��6%   $#��Срок выполнения��65   $��#Jost)�6L   $��16(�6_   $��8,38,12,-16q�6m   $��2��6�   �� 5)      $��6��6   $��25��6X   $��#Jost)�6d   $��16(�6w   $��8,73,12,-46q�6�   ��6%   �� *
Deadline+ �� StringFormat$ 
dd.MM.yyyy��6�   �� 5+      -   5        $��6��5+      $��1��6"   $��
Назад��62   $��#Jost)�6B   $��0,138,10,-118q�6U   $��Right=�6l   $	��100��6�   6�   5,      5-      5.      