﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace Grupo4TPWinform
{
    public partial class frmListar : Form
    {
        private List<Articulo> listaArt;
        private List<Imagen> listaImagenes;
        public frmListar()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();       
        }


        private void cargar()
        {

            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArt = negocio.listar();
                dgvArticulos.DataSource = listaArt;
                dgvArticulos.Columns["url"].Visible = false;
                dgvArticulos.Columns["idArt"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = new Articulo();

            if (dgvArticulos.CurrentRow != null)
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.url.ToString());
            }
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulos.Load(imagen);
            }
            catch (Exception)
            {
                pbxArticulos.Load("https://static.vecteezy.com/system/resources/previews/004/141/669/original/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = new Articulo();
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmAlta modificar = new frmAlta(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
               Articulo seleccionado;
                try
                {
                    DialogResult respuesta = MessageBox.Show("Está por eliminar un registro de la base de datos. ¿Quiere seguir adelante?", "Proceso de eliminación...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (respuesta == DialogResult.Yes)
                    {
                        seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                        negocio.eliminarArticulo(seleccionado.idArt);
                        cargar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            frmAlta alta = new frmAlta();
            alta.ShowDialog();
            cargar();
        }
    }
}
