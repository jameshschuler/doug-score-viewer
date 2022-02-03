<template>
  <div class="column is-one-third-desktop is-full-touch">
    <div class="card is-relative">
      <div class="overlay has-text-centered">
        <div class="information">
          <h3 class="title has-text-white is-size-2">Hello</h3>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({});
</script>
<style lang="scss">
.card {
  min-height: 200px;
}

.overlay {
  background-color: black;
  opacity: 0.5;
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  margin-left: auto;
  margin-right: auto;
  width: 100%; /* Need a specific value to work */
  z-index: 2;
  height: 100%;

  .information {
    margin-top: 1rem;
    height: 100%;
    .title {
      text-overflow: ellipsis;
      overflow: hidden;
      white-space: nowrap;
      max-width: 95%;
      margin-left: auto;
      margin-right: auto;
    }
  }
}
</style>
