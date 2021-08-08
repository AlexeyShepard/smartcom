<template>
  <div id="app">
    <div class="container">
            <div class="columns is-multiline is-gapless">
                <div class="column is-full">
                    <Navbar />                   
                </div>
                <div class="column is-full">
                  <div class="container main">
                      <router-view></router-view>
                  </div>               
                </div>
            </div>      
        </div>
  </div>
</template>

<script>

import Navbar from './components/Navbar.vue'

export default {
  name: 'App',
  components: {
    Navbar
  }
}

</script>

<style>
    body {
        align-content: center;
        background-color: rgb(217, 223, 235);
    }
    .main {
        background-color: rgb(181, 196, 224);
        height: 1000px;
        align-content: center;
    }

</style>
