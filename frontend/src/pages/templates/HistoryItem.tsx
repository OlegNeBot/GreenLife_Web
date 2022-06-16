import React from 'react';
import ItemTemplate from './ItemTemplate';

interface HistoryProps {
  username: string,
  date: string,
  action: string
}

const HistoryItem : React.FC<HistoryProps> = props => {
  return(
    <>
      <ItemTemplate type='history' username={props.username} date={props.date} action={props.action}/>
    </>
  );
}

export default HistoryItem;