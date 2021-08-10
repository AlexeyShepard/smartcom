<!--Панель заказчиков-->
<template>
    <div>
        <h1>Панель заказчика</h1>
        <b-tabs content-class="mt-3">
            <ProductTab/>
            <OrderTab/>
        </b-tabs>
    </div>  
</template>

<script>

import ProductTab from './ProductTab.vue';
import OrderTab from './OrderTab.vue';

export default {
    components : {
        ProductTab,
        OrderTab
    },
    created() {
        if(localStorage.getItem("Busket") == null) localStorage.setItem("Busket", []);
    }
}

</script>
