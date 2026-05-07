interface PlanProps {
  nombre: string;
  precio: number;
  descripcion: string;
}

export function Plan({ nombre, precio, descripcion }: PlanProps) {
  return (
    <li className="list-group-item text-center">
      <div>
        <h5 className="mb-1 text-dark">{nombre}</h5>
        <p className="mb-1 text-secondary">{descripcion}</p>
        <small className="fw-bold text-success">Precio: {precio} Bs.</small>
      </div>
    </li>
  );
}