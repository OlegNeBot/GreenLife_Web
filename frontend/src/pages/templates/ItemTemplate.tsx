import React from 'react';
import { Card } from 'react-bootstrap';

interface ItemProps {
  type: string,
  achievement?: string,
  action?: string,
  username: string,
  date: string
}

const ItemTemplate : React.FC<ItemProps> = props => {
  let text = '';
  if (props.type === 'achievement') {
    text = `${props.username} получил(а) достижение: "${props.achievement}"!`;
  }
  else if (props.type === 'history') {
    text = `${props.username} ${props.action}!`
  }

  return(
    <>
      <Card>
        <Card.Body>
          <Card.Subtitle className='text-muted mb-2'>{props.date}</Card.Subtitle>
          <Card.Text>{text}</Card.Text>
        </Card.Body>
      </Card>
    </>
  );
}

export default ItemTemplate;