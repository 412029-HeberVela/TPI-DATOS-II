const BASE_URL = "https://localhost:7016/api/report";

// -------- HELPERS --------
const dias = {
    1: "Domingo",
    2: "Lunes",
    3: "Martes",
    4: "Miércoles",
    5: "Jueves",
    6: "Viernes",
    7: "Sábado"
};

function formatearPesos(valor) {
    return "$" + valor.toLocaleString("es-AR");
}

// -------- TABLA CLIENTES --------
async function tablaClientes() {
    const res = await fetch(`${BASE_URL}/pedidos-por-cliente`);
    const data = await res.json();

    console.log(data);

    const tabla = document.getElementById("tablaClientes");

    tabla.innerHTML = `
    <tr class="border-b">
      <th>Cliente</th>
      <th>Pedidos</th>
      <th>Total</th>
    </tr>
    ${data.map(c => `
      <tr class="text-center border-b">
        <td>${c.cliente}</td>
        <td>${c.cantidadPedidos}</td>
        <td>${formatearPesos(c.totalGastado)}</td>
      </tr>
    `).join("")}
  `;
}

// -------- TOP CLIENTES --------
async function graficoClientes() {
    const res = await fetch(`${BASE_URL}/top-clientes`);
    const data = await res.json();

    console.log(data);

    new Chart(document.getElementById("clientesChart"), {
        type: "bar",
        data: {
            labels: data.map(c => c.cliente),
            datasets: [{
                label: "Pedidos",
                data: data.map(c => c.cantidadPedidos)
            }]
        }
    });
}

// -------- PRODUCTOS --------
async function graficoProductos() {
    const res = await fetch(`${BASE_URL}/productos-mas-vendidos`);
    const data = await res.json();

    console.log(data);

    new Chart(document.getElementById("productosChart"), {
        type: "bar",
        data: {
            labels: data.map(p => p.producto),
            datasets: [{
                label: "Cantidad",
                data: data.map(p => p.cantidadVendida)
            }]
        }
    });
}

// -------- DIAS --------
async function graficoDias() {
    const res = await fetch(`${BASE_URL}/ventas-por-dia-semana`);
    const data = await res.json();

    new Chart(document.getElementById("diasChart"), {
        type: "bar",
        data: {
            labels: data.map(d => dias[d.diaNumero]),
            datasets: [{
                label: "Pedidos",
                data: data.map(d => d.cantidadPedidos)
            }]
        }
    });
}

// -------- VENTAS --------
async function graficoVentas() {
    const res = await fetch(`${BASE_URL}/ventas-por-dia`);
    const data = await res.json();

    console.log(data);

    new Chart(document.getElementById("ventasChart"), {
        type: "line",
        data: {
            labels: data.map(v => v.fecha),
            datasets: [{
                label: "Ventas",
                data: data.map(v => v.totalVentas),
                tension: 0.3
            }]
        }
    });
}

// -------- ESTADOS --------
async function tablaEstados() {
    const res = await fetch(`${BASE_URL}/pedidos-por-estado`);
    const data = await res.json();

    console.log(data);

    const tabla = document.getElementById("tablaEstados");

    tabla.innerHTML = `
    <tr class="border-b">
      <th>Estado</th>
      <th>Cantidad</th>
    </tr>
    ${data.map(e => `
      <tr class="text-center border-b">
        <td>${e.estado}</td>
        <td>${e.cantidad}</td>
      </tr>
    `).join("")}
  `;
}

// -------- INIT --------
async function init() {
    await tablaClientes();
    await graficoClientes();
    await graficoProductos();
    await graficoDias();
    await graficoVentas();
    await tablaEstados();
}

init();