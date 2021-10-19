import React, { useState } from 'react';
import { useHttp } from '../hooks/http.hook'
import { Button, Form } from 'react-bootstrap';
import { Container, Row, Col } from 'reactstrap';

const default_client = {fullName: "" }

export const AddClientLine = (props) => {

    const [client, setClient] = useState({ ...default_client }); 
    const { request} = useHttp()
    const {fullName } = client;
    const handleOnSubmit = async (e) => {
        try {

            e.preventDefault();
            const data = await request('/api/queueLine/addclientline', 'POST', client);
            if (data && data.success) {
                props.refresh();
                setClient(default_client);
            } else {
                var mess = data && data.error ? data.error : '';
                console.log(mess);

            }
        } catch (e) {
            console.log(e);
        }
    }
    
    const handleInputChange = (event) => {
        setClient({ ...client, [event.target.name]: event.target.value });
    }
    const isButtonDisabled = fullName === '';
    return (
        <div className="shadow rounded p-3">
        <Container  fluid>
            <Form className="" onSubmit={(e) => handleOnSubmit(e)}>
                <Row>
                        <Col lg="6" sm="12">
                <Form.Group controlId="fullName">
                    <Form.Control
                        className="input-control"
                        type="text"
                        name="fullName"
                        value={fullName}
                        placeholder="Full name"
                        onChange={handleInputChange}
                    />
                    <Form.Text className="text-muted">
                        Name of the client you're going to add to the line
                    </Form.Text>
                        </Form.Group>
                        </Col>
                        <Col className="text-right mt-3" lg="6" sm="12" >
                            <Button variant="secondary" type="submit" className={`submit-btn ${isButtonDisabled ?'':'bg-green' } `} disabled={isButtonDisabled}>
                    + Add to the Line
                        </Button>
                        </Col>
                    </Row>
            </Form>
            </Container>
        </div>
    );
};

