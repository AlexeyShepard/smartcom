<template>
    <div>
        <h1>Панель администратора</h1>
        <b-tabs content-class="mt-3">
            <CustomerTab/>
            <b-tab title="Заказы"><p>ЗАКАЗЧИКИ</p></b-tab>
            <ProductTab/>
        </b-tabs>
    </div>     
</template>

<script>

import CustomerTab from './CustomerTab.vue';
import ProductTab from './ProductTab.vue';

export default {
    
    components : {
        CustomerTab,
        ProductTab
    }  
}

</script>