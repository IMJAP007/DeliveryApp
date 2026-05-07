using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeliveryApp
{
    public class DeliveryForm : Form
    {
        private DeliveryManager deliveryManager;
        private Label customerNameLabel;
        private TextBox customerNameTextBox;
        private Label addressLabel;
        private TextBox addressTextBox;
        private Label deliveryDateLabel;
        private DateTimePicker deliveryDatePicker;
        private Label statusLabel;
        private ComboBox statusComboBox;
        private Button addDeliveryButton;
        private Button removeDeliveryButton;
        private Button updateStatusButton;
        private Label deliveriesListLabel;
        private ListBox deliveriesListBox;
        private Label sortLabel;
        private ComboBox sortDeliveriesComboBox;
        private Button sort;
        private Button no_sort;
        public DeliveryForm()
        {
            this.Text = "Управление доставкой";
            this.Width = 600;
            this.Height = 500;
            customerNameLabel = new Label
            {
                Text = "Имя клиента",
                BackColor = System.Drawing.Color.Transparent,
                Location = new System.Drawing.Point(10, 10),
                Height = 20
            };
            customerNameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 30),
                Width = 150,
                //PlaceholderText = "Имя клиента"
            };
            addressLabel = new Label
            {
                Text = "Адрес доставки",
                BackColor = System.Drawing.Color.Transparent,
                Location = new System.Drawing.Point(170, 10),
                Height = 20
            };
            addressTextBox = new TextBox
            {
                Location = new System.Drawing.Point(170, 30),
                Width = 200,
                //PlaceholderText = "Адрес"
            };
            deliveryDateLabel = new Label
            {
                Text = "Дата доставки",
                BackColor = System.Drawing.Color.Transparent,
                Location = new System.Drawing.Point(380, 10),
                Height = 20
            };
            deliveryDatePicker = new DateTimePicker
            {
                Location = new System.Drawing.Point(380, 30)
            };
            statusLabel = new Label
            {
                Text = "Статус доставки",
                BackColor = System.Drawing.Color.Transparent,
                Location = new System.Drawing.Point(10, 70),
                Height = 20
            };
            statusComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(10, 90),
                Width = 100,
                Items = { "Новый", "В_пути", "Доставлен" }
            };
            addDeliveryButton = new Button
            {
                Location = new System.Drawing.Point(10, 130),
                Text = "Добавить",
                Width = 100
            };
            addDeliveryButton.Click += AddDeliveryButton_Click;
            removeDeliveryButton = new Button
            {
                Location = new System.Drawing.Point(120, 130),
                Text = "Удалить",
                Width = 100
            };
            removeDeliveryButton.Click += RemoveDeliveryButton_Click;
            updateStatusButton = new Button
            {
                Location = new System.Drawing.Point(220, 130),
                Text = "Обновить статус",
                Width = 120
            };
            updateStatusButton.Click += UpdateStatusButton_Click;
            deliveriesListLabel = new Label
            {
                Text = "Список доставок",
                BackColor = System.Drawing.Color.Transparent,
                Location = new System.Drawing.Point(10, 170),
                Height = 20
            };
            deliveriesListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 200),
                Width = 560,
                Height = 250
            };
            sortLabel = new Label
            {
                Text = "Фильтр по статусу",
                Location = new System.Drawing.Point(deliveriesListBox.Width - 365, 175),
                Height = 20,
                Width = 200
            };
            sortDeliveriesComboBox = new ComboBox
            {
                Width = 90,
                Height = 10,
                Location = new System.Drawing.Point(deliveriesListBox.Width-260, 171),
                Items = {"Новый", "В_пути", "Доставлен"},
                SelectedIndex = 0,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            sort = new Button
            {
                Text = "Выбрать",
                Width = 70,
                Height = 23,
                Location = new System.Drawing.Point(deliveriesListBox.Width - 160, 170)
            };
            sort.Click += Sort_Click;
            no_sort = new Button
            {
                Text = "Все доcтавки",
                Width = 100,
                Height = 23,
                Location = new System.Drawing.Point(deliveriesListBox.Width - 90, 170)
            };
            no_sort.Click += NoSort_Click;
            this.Controls.Add(customerNameLabel);
            this.Controls.Add(customerNameTextBox);
            this.Controls.Add(addressLabel);
            this.Controls.Add(addressTextBox);
            this.Controls.Add(deliveryDateLabel);
            this.Controls.Add(deliveryDatePicker);
            this.Controls.Add(statusLabel);
            this.Controls.Add(statusComboBox);
            this.Controls.Add(addDeliveryButton);
            this.Controls.Add(removeDeliveryButton);
            this.Controls.Add(updateStatusButton);
            this.Controls.Add(deliveriesListLabel);
            this.Controls.Add(deliveriesListBox);
            this.Controls.Add(sortDeliveriesComboBox);
            this.Controls.Add(sort);
            this.Controls.Add(no_sort);
            this.Controls.Add(sortLabel);
            deliveryManager = new DeliveryManager();
            UpdateDeliveriesList();
        }
        private void UpdateDeliveriesList()
        {
            deliveriesListBox.Items.Clear();
            foreach (var delivery in deliveryManager.Deliveries)
            {
                deliveriesListBox.Items.Add($"{delivery.CustomerName} - {delivery.Address} - {delivery.Status}");
            }
        }
        private void Sort_Click(object sender, EventArgs e)
        {
            var result = deliveryManager.SelectByStatus((DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), sortDeliveriesComboBox.SelectedItem?.ToString()));
            deliveriesListBox.Items.Clear();
            foreach (var delivery in result)
            {
                deliveriesListBox.Items.Add($"{delivery.CustomerName} - {delivery.Address} - {delivery.Status}");
            }
        }
        private void NoSort_Click(object sender, EventArgs e)
        {
            UpdateDeliveriesList();
        }
        private void AddDeliveryButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(customerNameTextBox.Text))
            {
                MessageBox.Show("Введите имя клиента!");
                return;
            }

            if (string.IsNullOrEmpty(addressTextBox.Text))
            {
                MessageBox.Show("Введите адрес доставки!");
                return;
            }
            DateTime deliveryDate = deliveryDatePicker.Value;
            Delivery newDelivery = new Delivery(customerNameTextBox.Text,
            addressTextBox.Text, deliveryDate);
            try
            {
                deliveryManager.AddDelivery(newDelivery);
                customerNameTextBox.Clear();
                addressTextBox.Clear();
                UpdateDeliveriesList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RemoveDeliveryButton_Click(object sender, EventArgs e)
        {
            if (deliveriesListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите доставку для удаления!");
                return;
            }
            string selectedItem = deliveriesListBox.SelectedItem.ToString();
            string[] parts = selectedItem.Split(new[] { '-' }, StringSplitOptions.None);
            if (parts.Length >= 2)
            {
                string customerName = parts[0].Trim();
                string address = parts[1].Trim();
                var deliveryToRemove = deliveryManager.Deliveries.Find(d => d.CustomerName ==
                customerName && d.Address == address);
                if (deliveryToRemove != null)
                {
                    try
                    {
                        deliveryManager.RemoveDelivery(deliveryToRemove);
                        UpdateDeliveriesList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void UpdateStatusButton_Click(object sender, EventArgs e)
        {
            if (deliveriesListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите доставку для обновления статуса!");
                return;
            }
            string selectedItem = deliveriesListBox.SelectedItem.ToString();
            string[] parts = selectedItem.Split(new[] { '-' }, StringSplitOptions.None);
            if (parts.Length >= 2)
            {
                string customerName = parts[0].Trim();
                string address = parts[1].Trim();
                var deliveryToUpdate = deliveryManager.Deliveries.Find(d => d.CustomerName ==
                customerName && d.Address == address);
                if (deliveryToUpdate != null)
                {
                    try
                    {
                        DeliveryStatus newStatus = (DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), statusComboBox.SelectedItem?.ToString());
                        deliveryManager.UpdateDeliveryStatus(deliveryToUpdate, newStatus);
                        UpdateDeliveriesList();
                    }
                    catch
                    {
                        MessageBox.Show("Статус достваки не может быть неопределенным!");
                    }
                }
            }
        }
    }
}
