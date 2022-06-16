import React from 'react';
import ProgressTemplate from './ProgressTemplate';

interface IComponentProps {
  num: number,
  total: number
}

const CheckListProgress : React.FC<IComponentProps> = props => {
  return(
    <>
      <ProgressTemplate type='checklist' num={props.num} total={props.total}/>
    </>
  );
}

export default CheckListProgress;