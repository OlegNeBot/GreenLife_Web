import React, {useState} from 'react';
import ListTemplate from './listsTemplates/ListTemplate';

const CheckLists : React.FC = () => {
    return(
        <>
          <ListTemplate type='checklist' />
        </>
    );
}

export default CheckLists;