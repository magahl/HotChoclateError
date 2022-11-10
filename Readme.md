### Query used

    query {
        shipments {
            nodes{
                shipmentId
                consignor {
                    addressLine1
                    name
                }
            }
        }
    }
