import React from 'react'
import { Table } from 'react-bootstrap';


export const ClientLinesList = ({ clientLines, setClientLines }) => {
    if (!clientLines || !Array.isArray(clientLines) || !clientLines.length) {
        return <p className="center">No Client Lines</p>
    } 

    return (
       

            <Table className="" >
                <thead>
                    <tr>
                        <th>Number in Line</th>
                        <th>Full Name</th>
                        <th>Check-in Time</th>
                    </tr>
                </thead>

                <tbody>
                    {clientLines.map((clientLine, index) => {
                        return (
                            <tr key={clientLine.id}>
                                <td>{clientLine.id}</td>
                                <td>{clientLine.fullName}</td>
                                <td>{clientLine.dateFormat}</td>
                            </tr>
                        )
                    })}
                </tbody>
            </Table>
            
    )
}
