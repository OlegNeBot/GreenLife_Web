import React, {useState} from 'react';
import ListTemplate from './listsTemplates/ListTemplate';

const Memos : React.FC = () => {
    return(
        <>
          <ListTemplate type='memo' />
        </>
    );
}

export default Memos;