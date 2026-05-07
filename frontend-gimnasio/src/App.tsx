import { ListaClientes } from "./components/Clientes/ListaClientes";
import { ListaPlanes } from "./components/Planes/ListaPlanes";

function App() {
  return (
    <div className="container mt-5">
      
      <div className="text-center mb-5">
        <h1 className="display-5 fw-bold text-primary">Sistema Gimnasio</h1>
        <hr className="w-25 mx-auto" />
      </div>

      <div className="row g-4">
        <div className="col-md-6">
          <div className="p-3 border rounded bg-light">
            <h3 className="h4 mb-3 text-center">Clientes</h3>
            <ListaClientes />
          </div>
        </div>

       
        <div className="col-md-6">
          <div className="p-3 border rounded bg-light">
            <h3 className="h4 mb-3 text-center">Planes</h3>
            <ListaPlanes />
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;