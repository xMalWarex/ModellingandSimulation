using System;

using System.Collections.Generic;

 

class CustomerQueue{

    Queue<Customer> customerQ = new Queue<Customer>();

 

    public void Push(Customer customer) {

        customerQ.Enqueue(customer);

    }

 

    public Customer Pop() {

        return customerQ.Dequeue();

    }

 

    public bool Empty() {

        return customerQ.Count == 0;

    }

 

    public int NumCustomers() {

        return customerQ.Count;

    }

}

 