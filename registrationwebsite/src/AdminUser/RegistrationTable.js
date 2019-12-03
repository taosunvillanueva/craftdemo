import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';

const useStyles = makeStyles({
  root: {
    width: '100%',
    overflowX: 'auto',
  },
  table: {
    minWidth: 650,
  },
});

function createData(name, calories, fat, carbs, protein) {
  return { name, calories, fat, carbs, protein };
}

export default function RegistrationTable(props) {
  const classes = useStyles();
  const items = props.data;

  return (
    <Paper className={classes.root}>
      <Table className={classes.table} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Name</TableCell>
            <TableCell align="right">Office Location</TableCell>
            <TableCell align="right">Email</TableCell>
            <TableCell align="right">Security Interest</TableCell>
            <TableCell align="right">T-Shirt Size</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {items.map(item => (
            <TableRow key={item.Name}>
              <TableCell component="th" scope="row">{item.Name}</TableCell>
              <TableCell align="right">{item.OfficeLocation}</TableCell>
              <TableCell align="right">{item.Email}</TableCell>
              <TableCell align="right">{item.SecurityInterest}</TableCell>
              <TableCell align="right">{item.ShirtSize}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </Paper>
  );
}