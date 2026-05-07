import { useState, useEffect } from 'react';
import axios from 'axios';
import { Cliente } from './Cliente';

interface ClienteData {
    idCliente: number;
    nombre: string;
    ci: number;
    activo: boolean;
}

export function ListaClientes() {
    const [clientes, setClientes] = useState<ClienteData[]>([]);

    useEffect(() => {
        
        axios.get('http://localhost:5024/api/clientes')
            .then(respuesta => setClientes(respuesta.data))
            .catch(error => console.error("Error:", error));
    }, []);

    return (
        <ul className="list-group ">
            {clientes.map(cliente => (
                <Cliente 
                    key={cliente.idCliente} 
                    nombre={cliente.nombre} 
                    ci={cliente.ci} 
                />
            ))}
        </ul>
    );
}