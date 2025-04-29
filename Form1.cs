using PedidosApp.clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PedidosApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click_Click(object sender, EventArgs e)
        {
            try
            {
                string cliente = txtCliente.Text;
                string producto = cmbProducto.SelectedItem?.ToString();
                bool urgente = chkUrgente.Checked;
                double peso = Convert.ToDouble(nudPeso.Value);
                int distancia = Convert.ToInt32(nudDistancia.Value);

                if (string.IsNullOrWhiteSpace(cliente) || string.IsNullOrWhiteSpace(producto))
                {
                    MessageBox.Show("Por favor, complete todos los campos.");
                    return;
                }

                Pedido pedido = new Pedido(cliente, producto, urgente, peso, distancia);
                RegistroPedidos.Instancia.AgregarPedido(pedido);

                lblResultado.Text = $"Entrega: {pedido.MetodoEntrega.TipoEntrega()} | " +
                                    $"Costo: ${pedido.ObtenerCosto():0.00}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnVerPedidos_Click_Click(object sender, EventArgs e)
        {
            dgvPedidos.DataSource = null;
            dgvPedidos.DataSource = RegistroPedidos.Instancia.Pedidos.Select(p => new
            {
                Cliente = p.Cliente,
                Producto = p.Producto,
                Urgente = p.Urgente ? "Sí" : "No",
                Peso = p.Peso,
                Distancia = p.Distancia,
                MetodoEntrega = p.MetodoEntrega.TipoEntrega(),
                Costo = p.ObtenerCosto()
            }).ToList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            #region[combo transporte]
            cmbProducto.Items.Add("TECNOLOGIA");
            cmbProducto.Items.Add("ACCESORIOS");
            cmbProducto.Items.Add("COMPONENTE");
            cmbProducto.SelectedIndex = -1;
            #endregion
        }
    }
}
