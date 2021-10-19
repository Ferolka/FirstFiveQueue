import React from 'react';
import { Button, Form } from 'react-bootstrap';
import { ClientLinesList } from './ClientLinesList';

export const InService = (props) => {

    const { client,handleNext } = props;
    

    return (
        <>
            <h1>In service</h1>
            <div className="shadow rounded p-3">
                <ClientLinesList clientLines={client?[client]:[]} />
                <div className="text-right">
                    {client &&
                        <Form onSubmit={(e) => handleNext(e)}>
                            <Form.Group controlId="id">
                                <Form.Control
                                    className="input-control"
                                    type="hidden"
                                    name="id"
                                    value={client.id}
                                />
                            </Form.Group>


                            <Button variant="primary" type="submit" className="bg-green">
                                Next Client
                            </Button>
                        </Form>
                    }
                </div>
            </div>
            </>
    );
};

