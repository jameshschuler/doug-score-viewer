<template>
  <div v-if="!store.loading">
    <Navbar />
    <main class="container is-fluid my-6">
      <div class="columns">
        <router-view class="column is-12-touch"></router-view>
      </div>
    </main>
    <Footer />
  </div>
  <LoadingOverlay v-if="store.loading" />
</template>
<script setup lang="ts">
import { useRouter } from "vue-router";
import Footer from "./components/Footer.vue";
import LoadingOverlay from "./components/LoadingOverlay.vue";
import Navbar from "./components/Navbar.vue";
import { AppErrorType } from "./models/enums/error";
import { store } from "./store";

const router = useRouter();

async function checkAPIHealth() {
  try {
    const healthCheckUrl = `${import.meta.env.VITE_API_BASE_URL}/health`;
    const response = await fetch(healthCheckUrl);
    const data = await response.text();
    console.info(`API responded with...${data}`);
    store.setLoading(false);

    window.onkeydown = function (ev: KeyboardEvent) {
      if (ev.ctrlKey && ev.key === "q") {
        store.toggleSearchDrawer();
      }
    };
  } catch (err) {
    store.setError({ errorType: AppErrorType.Server, message: (err as Error).message });
    store.setLoading(false);
    router.push("/error");
  }
}

checkAPIHealth();
</script>

<style lang="scss">
* {
  box-sizing: border-box;
}

html,
body {
  font-family: "Open Sans", sans-serif;
  background-color: hsl(60, 67%, 99%);
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

/* Tooltip container */
.tooltip {
  position: relative;
  display: inline-block;

  &:hover {
    .tooltiptext {
      visibility: visible;
    }
  }

  .tooltiptext {
    visibility: hidden;
    background-color: hsl(0, 0%, 0%);
    opacity: 0.9;
    color: hsl(0, 0%, 100%);
    text-align: center;
    padding: 5px 0;
    border-radius: 6px;
    width: 120px;
    bottom: 100%;
    left: 50%;
    margin-left: -60px; /* Use half of the width (120/2 = 60), to center the tooltip */
    position: absolute;
    z-index: 1;
  }
}

.tooltip .tooltiptext::after {
  content: " ";
  position: absolute;
  top: 100%; /* At the bottom of the tooltip */
  left: 50%;
  margin-left: -5px;
  border-width: 5px;
  border-style: solid;
  border-color: hsl(0, 0%, 0%) transparent transparent transparent;
}
</style>
