import React from 'react';
import { useLanguage } from '../../../contexts/LanguageContext';

import './Result.css';

export default function Result(props) {

    const { selectedLanguage } = useLanguage();


    return(
    <div className="card result">
        <a href={`/details/${props.document.id}`}>
            <img className="card-img-top" src={props.document.url} alt={props.document["nombre_" + selectedLanguage]}></img>
            <div className="card-body">
                <h6 className="title-style">{props.document["nombre_" + selectedLanguage]}</h6>
            </div>
        </a>
    </div>
    );
}
