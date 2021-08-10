<!--Панель администратора-->
<template>
    <div>
        <h1>Панель администратора</h1>
        <b-tabs content-class="mt-3">
            <CustomerTab/>
            <OrderTab/>
            <ProductTab/>
        </b-tabs>
    </div>     
</template>

<script>

import CustomerTab from './CustomerTab.vue';
import ProductTab from './ProductTab.vue';
import OrderTab from './OrderTab.vue';

export default {
    
    components : {
        CustomerTab,
        ProductTab,
        OrderTab
    }  
}

</script>