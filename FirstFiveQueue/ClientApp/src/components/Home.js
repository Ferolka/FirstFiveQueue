import React, { useCallback, /*useContext,*/ useEffect, useState } from 'react';
import { useHttp } from '../hooks/http.hook'
import { AddClientLine } from './AddClientLine';
import { ClientLinesList } from './ClientLinesList';
import { InService } from './InService';
export const Home  = (props) => {
    const [clientLines, setClientLines] = useState([])
    const [inService, setInService] = useState({"id":0})
    const { request } = useHttp()

    const fetchLine = useCallback(async () => {
        try {
            const fetched = await request('/api/queueLine/clientline', 'GET', null)
            setClientLines(fetched)
        } catch (e) {
            console.log(e);
        }
    }, [request])
    const fetchInService = useCallback(async () => {
        try {
            const fetched = await request('/api/queueLine/inservice', 'GET', null)
            setInService(fetched)
        } catch (e) {
            console.log(e);
        }
    }, [request])
    const handleNext = async (e) => {
        try {

            e.preventDefault();
            const data = await request('/api/queueLine/nextClient', 'POST', inService);
            if (data && data.success) {
                setInService(data.queueLine)
                fetchLine()
            } else {
                var mess = data && data.error ? data.error : '';
                console.log(mess);

            }
        } catch (e) {
            console.log(e);
        }
    }

    useEffect(() => {
        fetchLine()
        fetchInService();
    }, [fetchLine,fetchInService])
   
    const refresh = () => {
        fetchLine();
        fetchInService();
    }
    return (
        <div>
            <InService client={inService} handleNext={handleNext} />
            <h1>Clients in line</h1>
            <AddClientLine refresh={refresh}></AddClientLine>
            <br />
            <div className="shadow rounded p-3">
                <ClientLinesList clientLines={clientLines}></ClientLinesList>
            </div>
        </div>
    );
  
}
