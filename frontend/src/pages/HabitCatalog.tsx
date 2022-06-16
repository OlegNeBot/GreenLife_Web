import React, {useState} from 'react';
import ListTemplate from './listsTemplates/ListTemplate';

const HabitCatalog : React.FC = () => {
    return(
        <>
          <ListTemplate type='habit' />
        </>
    );
}

export default HabitCatalog;