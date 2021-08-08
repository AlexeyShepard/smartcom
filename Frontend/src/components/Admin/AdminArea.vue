<template>
    <div>
        <h1>Панель администратора</h1>
        <b-tabs content-class="mt-3">
            <CustomerTab/>
            <b-tab title="Заказы"><p>ЗАКАЗЧИКИ</p></b-tab>
            <b-tab title="Товары"><p>ТОВАРЫ</p></b-tab>
        </b-tabs>
    </div>     
</template>

<script>

import CustomerTab from './CustomerTab.vue'

export default {
    
    components : {
        CustomerTab
    }  
}

</script>