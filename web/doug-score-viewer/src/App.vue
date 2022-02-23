<script setup lang="ts">
import Navbar from "./components/Navbar.vue";
import Footer from "./components/Footer.vue";
import { ref } from "vue";
import LoadingOverlay from "./components/LoadingOverlay.vue";

const loading = ref<boolean>(true);

async function checkAPIHealth() {
  const healthCheckUrl = `${import.meta.env.VITE_API_BASE_URL}/health`;
  const response = await fetch(healthCheckUrl);
  const data = await response.text();
  console.info(`API responded with...${data}`);
  loading.value = false;
}

checkAPIHealth();
</script>

<template>
  <div v-if="!loading">
    <Navbar />
    <main class="container is-fluid my-6">
      <div class="columns">
        <router-view class="column is-12-touch"></router-view>
      </div>
    </main>
    <Footer />
  </div>

  <LoadingOverlay v-if="loading" />
</template>

<style lang="scss">
* {
  box-sizing: border-box;
}

html,
body {
  font-family: "Open Sans", sans-serif;
}

body {
  #app {
    display: flex;
    flex-direction: column;
    min-height: 100vh;
    > div {
      flex: 1;
      display: flex;
      flex-direction: column;
    }
  }
}

.is-red {
  color: hsl(0, 100%, 50%);

  &:hover {
    color: hsl(0, 100%, 50%);
  }
}

.has-border-great {
  border-color: hsl(141, 71%, 48%) !important;

  &:hover {
    background-color: hsl(141, 71%, 48%) !important;
    color: hsl(0, 0%, 100%);
  }
}

.has-border-okay {
  border-color: hsl(48, 100%, 67%) !important;

  &:hover {
    background-color: hsl(48, 100%, 67%) !important;
    color: hsl(0, 0%, 100%);
  }
}

.has-border-sad {
  border-color: hsl(348, 100%, 61%) !important;

  &:hover {
    background-color: hsl(348, 100%, 61%) !important;
    color: hsl(0, 0%, 100%);
  }
}
</style>
