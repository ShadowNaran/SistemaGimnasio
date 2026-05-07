interface ClienteProps {
  nombre: string;
  ci: number;
}

export function Cliente({ nombre, ci }: ClienteProps) {
  return (
    <li className="list-group-item text-center">
      <div>
        <h5 className="mb-1 text-dark">{nombre}</h5>
        <small className="text-muted">CI: {ci}</small>
      </div>
    </li>
  );
}